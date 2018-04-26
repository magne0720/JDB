using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour {

    public static bool ConfigActive;

	// Use this for initialization
	void Start () {
        ConfigActive = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))//スタートボタン
        {
            SceneManager.LoadSceneAsync("GameScene");
        }
        if (!ConfigActive && Input.GetKeyDown(KeyCode.Alpha2))//コンフィグ
        {
            SceneManager.LoadSceneAsync("ConfigScene", LoadSceneMode.Additive);
            ConfigActive = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))//終了
        {
            //Application.Quit();//ゲームを終了する処理
            Debug.Log("閉じる");
        }
    }

    public static void setConfigActive(bool b)
    {
        ConfigActive = b;
    }
}
