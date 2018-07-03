using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//ゲーム全体の管理
public class GameManager : MonoBehaviour {

    //ゲーム状態
    public enum GAME_MODE
    {
        TITLE, GAME, CLEAR, MISS
    }
    //ゲーム状態の変数
    public GAME_MODE gameMode;
    //制限時間
    public float GameClearTime = 300.0f;
    //経過時間
    public float GamePlayTimer = 0.0f;

    public bool isGameStandby = false;//タイトルからスタートボタンへ
    public bool isGameStartStandby = false;//スタートボタンの明転から暗転してゲームスタートへ

    public static bool isAttacked;//攻撃されているか

    public PostEffect effect;

    //ゲーム開始用のスタートボタンオブジェクト
    public IsRendered StartButton;

    public List<TextManager> text;

    public SpawnManager spawn;

    public static bool isGameOver = false;

    public List<GameObject> NormalItems;

    public int AddTime = 120;
    // Use this for initialization
    void Start() {
        effect.Depth = 0.0f;
        GamePlayTimer = 0.0f;
        gameMode = GAME_MODE.TITLE;
        NormalItems = new List<GameObject>();
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("NormalItem"))
        {
            NormalItems.Add(g);
        }
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {

        switch (gameMode)
        {
            //ゲーム開始の待機
            case GAME_MODE.TITLE:
                if (isGameStandby)
                {
                    GameStart();
                    if (OVRInput.GetDown(OVRInput.RawButton.A) || Input.GetKeyDown(KeyCode.Return))
                    {
                        if (effect.damage == 1)
                        {
                            foreach (TextManager t in text)
                                if (t.changeText())
                                {
                                    isGameStartStandby = true;
                                }
                        }
                    }
                }
                if (StartButton.isCaptioned)
                {
                    isGameStandby = true;
                    foreach (TextManager t in text)
                        t.gameObject.SetActive(true);
                }
                break;
            //ゲーム中
            case GAME_MODE.GAME:
                GamePlayTimer += Time.deltaTime;
                if ((int)GamePlayTimer % AddTime == 0 && GamePlayTimer / AddTime > spawn.putCount)
                {
                    spawn.Spawn(1);
                    Debug.Log("dfaf");
                }
                CheckGhostAttack();
                //クリア条件
                if (GamePlayTimer >= GameClearTime)
                {
                    gameMode = GAME_MODE.CLEAR;
                }
                if (isGameOver)
                {
                    gameMode = GAME_MODE.MISS;
                }
                break;
            //ゲームクリア
            case GAME_MODE.CLEAR:
                if (effect.damage <= 1.0f)
                {
                    effect.damage += 0.02f;
                }
                else
                {
                    GamePlayTimer = 0.0f;
                    isGameStandby = false;
                    isGameStartStandby = false;
                    StartButton.isCaptioned = false;
                    foreach (TextManager t in text)
                    {
                        t.ResetCountText();
                        t.gameObject.SetActive(true);
                        t.SetTextColor(Color.black);
                        t.SetText("朝になり帰ることができるようになった。\nAボタンでタイトル");
                    }
                    if (OVRInput.GetDown(OVRInput.RawButton.A) || Input.GetKeyDown(KeyCode.Return))
                    {
                        SceneManager.LoadScene(0);
                    }
                }
                break;
            //ゲームオーバー
            case GAME_MODE.MISS:
                if (effect.damage >= -1.0f)
                {
                    effect.damage -= 0.02f;
                }
                else
                {
                    GamePlayTimer = 0.0f;
                    isGameStandby = false;
                    isGameStartStandby = false;
                    StartButton.isCaptioned = false;
                    foreach (TextManager t in text)
                    {
                        t.ResetCountText();
                        t.gameObject.SetActive(true);
                        t.SetTextColor(Color.white);
                        t.SetText("あなたは死んでしまった。\nAボタンでタイトル");
                    }
                    if (OVRInput.GetDown(OVRInput.RawButton.A) || Input.GetKeyDown(KeyCode.Return))
                    {
                        SceneManager.LoadScene(0);
                    }
                }
                break;
            default:
                break;
        }
    }
    //ゲーム開始の初期化
    void GameStart()
    {
        if (isGameStandby)
        {
            if (isGameStartStandby)
            {
                if (effect.damage >= -0.2f)
                {
                    effect.damage -= 0.05f;
                }
                if (effect.damage < 0.2f)
                {
                    effect.damage = -0.2f;
                    GameStartInit();
                }
            }
            else
            {
                if (effect.damage <= 1.0f)
                {
                    effect.damage += 0.05f;
                }
                if (effect.damage > 1.0f)
                {
                    effect.damage = 1.0f;
                }
            }
        }
    }

    //ゲーム開始
    void GameStartInit()
    {
        StartButton.setObjectActive(false);
        gameMode = GAME_MODE.GAME;
        spawn.OriginalSpawn(10);
        spawn.Spawn(1);

        foreach (GameObject g in NormalItems)
        {
            foreach (GameObject o in spawn.SpawnEnemys)
            {
                g.GetComponent<NormalItem>().addReactionObject(o);
            }
        }
    }

    //ゲームオーバー時
    public static void GameOver()
    {
        isGameOver = true;
    }

    //ポーズ時の変更
    void Pause()
    {

    }

    void CheckGhostAttack()
    {
        bool isAttack = false;

        foreach (GameObject g in spawn.SpawnEnemys)
        {
            if (g.GetComponent<EnemyControl>().isLastAttack)
            {
                //誰かが最終まで到達している
                isAttack = true;
                break;
            }
        }
        if (isAttack)
        {
            if (effect.Depth <= 1.5f)
                effect.Depth += 0.01f;
        }
        else if (effect.Depth > 0.0f)
        {
            effect.Depth -= 0.03f;
        }
        else
        {
            effect.Depth = 0.0f;
        }
    }
}
