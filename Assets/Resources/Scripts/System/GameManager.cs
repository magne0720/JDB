using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ゲーム全体の管理
public class GameManager : MonoBehaviour {

    //ゲーム状態
    public enum GAME_MODE
    {
        TITLE,GAME,CLEAR,MISS
    }
    //ゲーム状態の変数
    public GAME_MODE gameMode;
    //制限時間
    public float GameClearTime = 300.0f;
    //経過時間
    public float GamePlayTimer = 0.0f;


    //ゲーム開始用のスタートボタンオブジェクト
    public IsRendered StartButton;

	// Use this for initialization
	void Start () {

        GamePlayTimer = 0.0f;
        gameMode = GAME_MODE.TITLE;



	}
	
	// Update is called once per frame
	void Update () {

        switch (gameMode)
        {
            //ゲーム開始の待機
            case GAME_MODE.TITLE:
                if (StartButton.isCaption)
                {
                    GameStart();
                }
                break;
            //ゲーム中
            case GAME_MODE.GAME:
                GamePlayTimer += Time.deltaTime;

                //クリア条件
                if (GamePlayTimer >= GameClearTime)
                {
                    gameMode = GAME_MODE.CLEAR;
                }
                break;
            //ゲームクリア
            case GAME_MODE.CLEAR:
                gameMode = GAME_MODE.TITLE;
                break;
            //ゲームオーバー
            case GAME_MODE.MISS:
                gameMode = GAME_MODE.TITLE;
                break;
            default:
                break;
        }
    }
    //ゲーム開始の初期化
    void GameStart()
    {
        gameMode = GAME_MODE.GAME;
    }

    //ゲームオーバー時
    void GameOver()
    {

    }

    //ポーズ時の変更
    void Pause()
    {

    }
}
