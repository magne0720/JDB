using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

    public Image ImgTitle, ImgResume;
    public Sprite TexTitle, TexTitleSub, TexResume, TexResumeSub;

    public bool select = false;

    public CanvasGroup canvas;
    public Image BlackBoard;
    private float time;
    private float BlackBoardAlpha;
    private bool CanvasFade;
    private bool BlackFade;

	// Use this for initialization
	void Start () {
        ImgTitle = ImgTitle.GetComponent<Image>();
        ImgResume = ImgResume.GetComponent<Image>();

        canvas = canvas.GetComponent<CanvasGroup>();
        BlackBoard = BlackBoard.GetComponent<Image>();
        BlackBoardAlpha = 0;
        time = 0;
        CanvasFade = true;
        BlackFade = true;
    }
	
	// Update is called once per frame
	void Update () {
        //Canvasの透過操作
        if(CanvasFade) time += Time.deltaTime * 4.0f;
        else time -= Time.deltaTime * 4.0f;
        canvas.alpha = time;

        //黒いやつの透過操作
        if (!BlackFade) BlackBoardAlpha += Time.deltaTime;// * 4.0f;
        BlackBoard.color = new Color(0, 0, 0, BlackBoardAlpha);

        //セレクト
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            select = !select;
        }

        //選択されているボタンの表示
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

        //エンター押時の処理
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (select)
            {
                BlackBoardAlpha = 0.0f;
                BlackFade = false;
                //SceneManager.LoadSceneAsync("TitleScene");
                GameMenuPush.setMenuActive(false);
            }
            else
            {
                CanvasFade = false;
                time = 1.0f;
                //SceneManager.UnloadSceneAsync("MenuScene");
                GameMenuPush.setMenuActive(false);
            }
        }

        //シーンの移動
        if(time < 0) SceneManager.UnloadSceneAsync("MenuScene");
        if(BlackBoardAlpha > 1) SceneManager.LoadSceneAsync("TitleScene");
    }
}
