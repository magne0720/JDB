using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopUpScript : MonoBehaviour {


    bool PopUpState;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (!PopUpState)
            {
                Application.LoadLevelAdditive("MenuScene");
                PopUpState = true;
            }
            else
            {
                Application.UnloadLevel("MenuScene");
                PopUpState = false;
                Resources.UnloadUnusedAssets();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (!PopUpState)
            {
                Application.LoadLevelAdditive("GameOverScene");
                PopUpState = true;
            }
            else
            {
                Application.UnloadLevel("GameOverScene");
                PopUpState = false;
                Resources.UnloadUnusedAssets();
            }

        }

    }
}
