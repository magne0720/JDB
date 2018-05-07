using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//見えない敵クラス
public class EnemyControl : IsRendered {

    public GameObject player;

    public enum ENEM_STATUS
    {
        STAND, WALK, CHASE,ATTAK, STOP
    }
    public ENEM_STATUS currentStatus;
    public ENEM_STATUS nextStatus;


    // Use this for initialization
    void Start () {
        gameObject.layer = 9;
        currentStatus = ENEM_STATUS.STAND;
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
                CharacterSearch();
                break;
            case ENEM_STATUS.WALK:
                CharacterSearch();
                break;
            case ENEM_STATUS.CHASE:
                //transform.Translate(new Vector3(0, 0, Time.deltaTime ));
                break;
            case ENEM_STATUS.ATTAK:
                break;
            case ENEM_STATUS.STOP:
                break;
            default:
                break;
        }

        //次回更新に一度の処理
        if (currentStatus != nextStatus)
        {
            switch (currentStatus)
            {
                case ENEM_STATUS.STAND:
                    break;
                case ENEM_STATUS.WALK:
                    break;
                case ENEM_STATUS.CHASE:
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
    }
    //目の前にあるキャラクターをリストにする
    void CharacterSearch()
    {
        if (player != null)
        {
            nextStatus = ENEM_STATUS.CHASE;
        }
    }
    public override void Caption()
    {
        Damage();
    }
    public void Damage()
    {
        nextStatus = ENEM_STATUS.STAND;
        Debug.Log("Damage");
        transform.Translate(Vector3.up);
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
