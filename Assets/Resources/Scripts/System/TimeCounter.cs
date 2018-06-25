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
            setTimer(15);
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
