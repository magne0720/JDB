using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//移行済み

public class TitleMenuScript : MonoBehaviour {

    public string SceneName = null;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GameSceneTransiton()
    {
        SceneManager.LoadScene(SceneName);
    }

    public void ConfigSceneLayer()
    {
        SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
    }
    public void CloseGame()
    {
        //Application.Quit();//ゲームを終了する処理
        Debug.Log("閉じる");
    }
}
