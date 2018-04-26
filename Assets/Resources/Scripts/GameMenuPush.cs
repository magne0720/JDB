using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuPush : MonoBehaviour {

    public static bool menu_active;

	// Use this for initialization
	void Start () {
        menu_active = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!menu_active && Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MenuScene", LoadSceneMode.Additive);
            menu_active = true;
        }
	}

    public static void setMenuActive(bool b)
    {
        menu_active = b;
    }
}
