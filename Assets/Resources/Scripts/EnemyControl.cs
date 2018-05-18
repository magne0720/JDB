using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//見えない敵クラス
public class EnemyControl : IsRendered {

    public float speed;
    public GameObject player;

    public enum ENEM_STATUS
    {
        STAND, WALK, CHASE,ATTAK, STOP
    }
    public ENEM_STATUS currentStatus;
    public ENEM_STATUS nextStatus;
    public float StateTimer;


    // Use this for initialization
    void Start ()
    {
        gameObject.layer = 9;//Ghost
        currentStatus = ENEM_STATUS.STAND;
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
	}
    void State()
    {
        //毎回更新
        switch (currentStatus)
        {
            case ENEM_STATUS.STAND:
                transform.Rotate(new Vector3(0, 5, 0));
                WallSearch();
                CharacterSearch();
                break;
            case ENEM_STATUS.WALK:
                WallSearch();
                CharacterSearch();
                transform.Translate(new Vector3(0, 0,speed* Time.deltaTime));
                //ChangeState(ENEM_STATUS.WALK, 6);
                break;
            case ENEM_STATUS.CHASE:
                if (player == null) { ChangeState(ENEM_STATUS.STAND, 0); return; }
                WallSearch();
                transform.LookAt(player.transform.position);
                transform.Translate(new Vector3(0, 0,speed* Time.deltaTime ));
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
        if (currentStatus != nextStatus && StateTimer<=0)
        {
            switch (currentStatus)
            {
                case ENEM_STATUS.STAND:
                    transform.Rotate(new Vector3(0, Random.Range(-180, 180), 0));
                    ChangeState(ENEM_STATUS.WALK, 4);
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
        StateTimer -= Time.deltaTime;
    }
    //目の前が壁だったら右か左に回転する
    void WallSearch()
    {
        //未完成
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.BoxCast(transform.position, transform.localScale, transform.forward, out hit, Quaternion.identity, 5.0f))
        {
            //Debug.Log("HIT,"+hit.collider.gameObject.name);
            if (hit.collider.gameObject.tag == "Wall")
            {
                ChangeState(ENEM_STATUS.STAND,3);
            }
            if (hit.collider.gameObject.tag == "Glass")
            {
                transform.Rotate(new Vector3(0, speed * 0.5f, 0));
            }
        }else if (currentStatus == ENEM_STATUS.STAND)
        {
            ChangeState(ENEM_STATUS.WALK);
        }
    }
    //目の前にあるキャラクターをリストにする
    void CharacterSearch()
    {
        //未完成
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.BoxCast(transform.position,Vector3.one,transform.forward,out hit))
        {
            Debug.Log("HIT," + hit.collider.gameObject.name);
            if (hit.collider.gameObject.tag == "Player")
            {
                player = hit.collider.gameObject;
            }
            if (player != null)
            {
                ChangeState(ENEM_STATUS.CHASE);
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
    void ChangeState(ENEM_STATUS s,float timer=0)
    {
        StateTimer = timer;

        nextStatus = s;
        
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
