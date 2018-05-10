using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

    public Image ImgTitle, ImgResume;
    public Sprite TexTitle, TexTitleSub, TexResume, TexResumeSub;

    public bool select = false;

	// Use this for initialization
	void Start () {
        ImgTitle = ImgTitle.GetComponent<Image>();
        ImgResume = ImgResume.GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            select = !select;
        }

        if (select)
        {
            ImgTitle.sprite = TexTitleSub;
            ImgResume.sprite = TexResume;
        }
        else
        {
            ImgTitle.sprite = TexTitle;
            ImgResume.sprite = TexResumeSub;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (select)
            {
                SceneManager.LoadSceneAsync("TitleScene");
                GameMenuPush.setMenuActive(false);
            }
            else
            {
                SceneManager.UnloadSceneAsync("MenuScene");
                GameMenuPush.setMenuActive(false);
            }
        }
    }
}
