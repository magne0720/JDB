using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCounter : MonoBehaviour {
    [Header ("制限時間(秒)")]
    public float TimeLimit;
    bool StartFlag = false;
    bool ClearFlag = false;
	// Use this for initialization
	void Start () {
        Application.targetFrameRate = 90;
        setTimer();
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            StartTimer(!StartFlag);
        }
        if (StartFlag) StartTimer();
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
    public void setTimer(float t = 30)//制限時間の指定(何も指定なしなら30秒)
    {
        TimeLimit = t;
    }
    public void StartTimer(bool b)//trueをsetでタイマースタート
    {
        StartFlag = b;
    }

    public void ClearGame()//クリア時の処理
    {

    }
}
