using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCounter : MonoBehaviour {
    [Header ("制限時間(秒)")]
    public float TimeLimit;
    [Header("ライト")]
    public GameObject light;

    GameManager gameManager;

    //Quaternion q1, q2;
    float lerpValue;
    bool StartFlag = false;
    bool ClearFlag = false;
	// Use this for initialization
	void Start () {
        gameManager = GetComponent<GameManager>();
        //Application.targetFrameRate = 90;

        setTimer();
	}
	
	// Update is called once per frame
	void Update () {
        //if(Input.GetKeyDown(KeyCode.Return))
        //{
        //    StartTimer(!StartFlag);
        //    setTimer(15);
        //}
        //if (StartFlag) StartTimer();
        if (gameManager.gameMode == GameManager.GAME_MODE.GAME)
        {
            // float lerpValue = gameManager.GamePlayTimer / gameManager.GameClearTime;
            // float lerpValue;
            lerpValue = gameManager.GamePlayTimer /gameManager.GameClearTime;
            Quaternion q1 = Quaternion.Euler(30f, 0f, 0f);
            Quaternion q2 = Quaternion.Euler(150f, 0f, 0f);
            light.transform.rotation = Quaternion.Lerp(q1, q2, lerpValue);
        }
    }

    void StartTimer()
    {
        if (TimeLimit > 0)
        {
            TimeLimit -= Time.deltaTime;
        }else
        {
            ClearFlag = true;
            Debug.Log(ClearFlag);
        }
    }
    /// <summary>
    /// 制限時間の指定がないなら30秒に自動設定
    /// </summary>
    /// <param name="t"></param>
    public void setTimer(float t = 30)
    {
        TimeLimit = t;
    }
    public void StartTimer(bool b)//trueで制限時間進、falseでストップ(ポーズ中とか)
    {
        StartFlag = b;
    }

    public void ClearGame()//クリア時の処理
    {

    }
}
