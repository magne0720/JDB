using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))//ゲームに戻る
        {
            SceneManager.UnloadSceneAsync("MenuScene");
            GameMenuPush.setMenuActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))//ゲームに戻る
        {
            SceneManager.UnloadSceneAsync("MenuScene");
            GameMenuPush.setMenuActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))//タイトルに戻る
        {
            SceneManager.LoadSceneAsync("TitleScene");
            GameMenuPush.setMenuActive(false);
        }
    }
}
