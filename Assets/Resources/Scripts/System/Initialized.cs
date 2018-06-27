using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Initialized : MonoBehaviour {

    static int clearCount;
    static int gameoverCount;

    void Start()
    {
        clearCount = gameoverCount = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))//debug用
        {
            SceneManager.LoadScene("GameScene");
        }
    }

    void reset( bool clear)//clear or gameover
    {
        if (clear) clearCount++;
        else gameoverCount++;

        //Debug.Log("sceneReset");
        SceneManager.LoadScene("GameScene");//再読み込み
    }
}
