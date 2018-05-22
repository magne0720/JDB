using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class text : MonoBehaviour
{

    TextAsset TextOrigine;  //テキストそのものをロード
    string[] textLine;  //テキストを分割したものを代入
    int currentLine;//表示するテキストの番号

    Text TextBox;        //テキストのオブジェクト

    int backLine = 0;   //戻る行数
    int LineLog = 0;    //currentLine比較用

    static string nextText = "tutorial_01";


    void Start()
    {
        TextBox = GameObject.Find("Text_test").GetComponent<Text>();
        currentLine = 0;
        backLine = 0;
        LineLog = currentLine;
        textIn(nextText); //最初にロードするテキストファイル
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            Debug.Log("enter");
            setText();
        }

        /*
        else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            setText();
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) && backLine != 0)
        {
            BackLine();
            setText();
        }
        */
    }

    //テキストロード
    void textIn(string title)
    {
        TextOrigine = Resources.Load<TextAsset>("text/" + title);   //テキストを取得
        string[] serch = { "@@" };                              //テキストの分割条件
        textLine = TextOrigine.text.Split(serch, System.StringSplitOptions.RemoveEmptyEntries); //配列に代入
        setText();

        for (int i = 0; i < textLine.Length; ++i)
        {
            Debug.Log(textLine[i]);
        }
    }

    //テキストを表示
    void setText()
    {
        //次のテキストへ進む
        /*
        if (textLine[currentLine].StartsWith("change_"))
        {
            currentLine++;
            nextText = textLine[currentLine];
            Debug.Log(nextText);
            Reset();
            //textIn(nextText);
        }
        */
        //else
        {
            //テキストを表示
            string s_Text = textLine[currentLine];
            TextBox.text = s_Text;//.Substring(2);

            backLine = currentLine - LineLog;
            LineLog = currentLine;

            if (backLine < 0)
            {
                backLine = 0;
            }

            currentLine++;
            if (currentLine == textLine.Length) currentLine = 0;

            Debug.Log("次回：" + currentLine + "表示：" + LineLog + "戻る：" + backLine);
        }

    }


    //会話を一行戻す
    void BackLine()
    {
        backLine *= 2;
        currentLine -= backLine;
        LineLog -= backLine;

        if (currentLine <= 0)
        {
            currentLine = 0;
            LineLog = 0;
        }
        backLine = currentLine - LineLog;
    }

    //会話終了時のリセット
    void Reset()
    {
        //EventSystem.limit++;
        //EventSystem.EventStop = true;
        currentLine = 0;
        LineLog = 0;
        backLine = 0;
        TextBox.text = "";
    }
}
