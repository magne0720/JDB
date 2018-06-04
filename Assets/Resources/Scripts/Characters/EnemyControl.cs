using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//見えない敵クラス
public class EnemyControl : IsRendered {

    public float speed;
    public GameObject player;
    public int loiterCount;
    public Vector3 myPos;
    public Vector3 loiter;
    public float debugdis;
    public List<Vector3> loiteringPoints;//徘徊位置

    public enum ENEM_STATUS
    {
        NONE,STAND,ROTATE, WALK, CHASE,ATTACK, STOP
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
        NextPoint();
	}
	
	// Update is called once per frame
	void Update () {
        myPos = transform.localPosition;
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
            gameObject.layer = 10;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Caption();  
        }
        debugdis = Vector3.Distance(transform.localPosition, loiter);
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
                ChangeStateReservation(ENEM_STATUS.WALK,1.5f);
                //WallSearch();
                CharacterSearch();
                //ChangeStateReservation(ENEM_STATUS.WALK, 1.5f);
                break;
            case ENEM_STATUS.WALK:
                transform.LookAt(loiter-transform.position);
                transform.Translate(new Vector3(0, 0,speed* Time.deltaTime));
                WallSearch();
                CharacterSearch();
                if (Vector3.Distance(loiter,transform.localPosition) <= 4.0f)
                {
                    ChangeStateReservation(ENEM_STATUS.ROTATE);
                    Debug.Log("next");
                }
                //ChangeState(ENEM_STATUS.WALK, 6);
                break;
            case ENEM_STATUS.CHASE:
                if (player != null)
                {
                    //WallSearch();
                    transform.LookAt( player.transform.localPosition);
                    transform.Translate(new Vector3(0, 0, speed*1.25f * Time.deltaTime));
                    if (Vector3.Distance(transform.position, player.transform.position) > 7.0f)
                    {
                        player = null;
                    }
                }
                else
                {
                    ChangeStateReservation(ENEM_STATUS.STAND);
                }
                Debug.Log("<color=red>chase</color>");
                break;
            case ENEM_STATUS.ATTACK:
                break;
            case ENEM_STATUS.STOP:
                ChangeStateReservation(ENEM_STATUS.WALK, 5);
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
                    NextPoint();
                    //ChangeStateReservation(ENEM_STATUS.WALK,2.0f);
                    break;
                case ENEM_STATUS.WALK:
                    break;
                case ENEM_STATUS.CHASE:
                    gameObject.layer = 10;
                    break;
                case ENEM_STATUS.ATTACK:
                    break;
                case ENEM_STATUS.STOP:
                    ChangeStateReservation(ENEM_STATUS.STAND, 5);
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
                if (hit.collider.gameObject.tag == "Wall")
                {
                    ChangeStateReservation(ENEM_STATUS.STAND);
                }
                if (hit.collider.gameObject.tag == "Glass")
                {
                    ChangeStateReservation(ENEM_STATUS.STAND);
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

        //5.0fはデバッグの大きさ
        if (Physics.BoxCast(transform.position,Vector3.one*5.0f,transform.forward,out hit))
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
        ChangeStateReservation(ENEM_STATUS.STOP);
        Debug.Log("Damage");
        //transform.Translate(Vector3.up);
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
    //次の指定ポイントを決める
    void NextPoint()
    {
        loiterCount++;
        if (loiterCount >= loiteringPoints.Count)
            loiterCount = 0;

        loiter = loiteringPoints[loiterCount];

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
