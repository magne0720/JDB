using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//見えない敵クラス
public class EnemyControl : IsRendered {

    public float speed;
    public GameObject player;

    public enum ENEM_STATUS
    {
        NONE,STAND,ROTATE, WALK, CHASE,ATTAK, STOP
    }
    public ENEM_STATUS currentStatus;
    public ENEM_STATUS nextStatus;
    public float StateTimer;
    public float ProgressTimer;
    
    // Use this for initialization
    void Start ()
    {
        gameObject.layer = 9;//Ghost
        currentStatus = ENEM_STATUS.STAND;
        ChangeState(ENEM_STATUS.WALK);
        speed = 4.0f;
        StateTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (player != null)
        {
            transform.LookAt(player.transform.position);
        }
        State();
        //デバッグ領域
        if (Input.GetKeyDown(KeyCode.K))
        {
            ChangeState(ENEM_STATUS.WALK);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            ChangeState(ENEM_STATUS.STAND);
        }
        //------------------------------
    }
    void State()
    {
        //毎回更新
        switch (currentStatus)
        {
            case ENEM_STATUS.STAND:
                WallSearch();
                CharacterSearch();
                break;
            case ENEM_STATUS.ROTATE:
                //WallSearch();
                CharacterSearch();
                ChangeStateReservation(ENEM_STATUS.WALK,1.5f);
                break;
            case ENEM_STATUS.WALK:
                transform.Translate(new Vector3(0, 0,speed* Time.deltaTime));
                WallSearch();
                CharacterSearch();
                //ChangeState(ENEM_STATUS.WALK, 6);
                break;
            case ENEM_STATUS.CHASE:
                if (player == null)
                { ChangeStateReservation(ENEM_STATUS.STAND, 0); return; }

                WallSearch();
                transform.LookAt(player.transform.position);
                transform.Translate(new Vector3(0, 0,speed* Time.deltaTime ));
                Debug.Log("<color=red>chase</color>");
                break;
            case ENEM_STATUS.ATTAK:
                break;
            case ENEM_STATUS.STOP:
                WallSearch();
                break;
            default:
                break;
        }

        //次回更新に一度の処理
        if (currentStatus != nextStatus && ProgressTimer >= StateTimer)
        {
            switch (currentStatus)
            {
                case ENEM_STATUS.STAND:
                    ChangeStateReservation(ENEM_STATUS.ROTATE);
                    break;
                case ENEM_STATUS.ROTATE:
                    transform.Rotate(new Vector3(0, Random.Range(-90, 90), 0));
                    //ChangeStateReservation(ENEM_STATUS.WALK);
                    break;
                case ENEM_STATUS.WALK:
                    break;
                case ENEM_STATUS.CHASE:
                    gameObject.layer = 10;
                    break;
                case ENEM_STATUS.ATTAK:
                    break;
                case ENEM_STATUS.STOP:
                    break;
                default:
                    break;
            }
            currentStatus = nextStatus;
        }
        ProgressTimer += Time.deltaTime;
    }
    //目の前が壁だったら右か左に回転する
    void WallSearch()
    {
        //未完成
        Ray ray = new Ray(transform.position, transform.forward*5.0f);
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.forward * 5.0f);

        if (currentStatus == ENEM_STATUS.WALK)
        {
            //if (Physics.BoxCast(transform.position, Vector3.one, transform.forward, out hit, Quaternion.identity, 1.0f))
            if (Physics.Raycast(ray, out hit, 5.0f))
            {
                //Debug.Log("HIT,"+hit.collider.gameObject.name);
                if (hit.collider.gameObject.tag == "Wall")
                {
                    ChangeStateReservation(ENEM_STATUS.STAND);
                }
                if (hit.collider.gameObject.tag == "Glass")
                {
                    ChangeStateReservation(ENEM_STATUS.STAND);
                    //transform.Rotate(new Vector3(0, speed * 0.5f, 0));
                }
            }
        }
        else if (currentStatus == ENEM_STATUS.STAND)
        {
            ChangeStateReservation(ENEM_STATUS.WALK);
        }
    }
    //目の前にあるキャラクターをリストにする
    void CharacterSearch()
    {
        //未完成
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.BoxCast(transform.position,transform.localScale,transform.forward,out hit))
        {
            //Debug.Log("HIT," + hit.collider.gameObject.name);
            if (hit.collider.gameObject.tag == "Player")
            {
                player = hit.collider.gameObject;
            }
            if (player != null)
            {
                ChangeStateReservation(ENEM_STATUS.CHASE);
            }
        }
    }
    public override void Caption()
    {
        Damage();
    }
    public void Damage()
    {
        ChangeState(ENEM_STATUS.STAND);
        Debug.Log("Damage");
        transform.Translate(Vector3.up);
    }

    //設定したtimerを超えると次の状態に移行
    //0予約で瞬時に変更
    void ChangeState(ENEM_STATUS s)
    {
        currentStatus = s;
    }
    void ChangeStateReservation(ENEM_STATUS s, float timer=0)
    {
        StateTimer = timer;

        if (currentStatus != s&&nextStatus!=s)
        {
            nextStatus = s;
            ProgressTimer = 0;
        }
    }
    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.layer==8)
        {
            //自身のタグを変える
            gameObject.layer=10;
        }
    }
}
