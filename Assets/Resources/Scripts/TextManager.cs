using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TextManager : MonoBehaviour
{

    TextAsset textOrigine;//テキストそのもの
    string[] textLine;//分割したテキストを格納
    int currentLine;//表示する行

    Text textBox;

    int backLine = 0;//戻り行数
    int LineLog = 0;//currentLine比較

    //スクリプトをぶち込むテキストオブジェクトの名前
    public string textBoxName="Text_test";

    //読み込ませたいスクリプトの名前
    public string textName = "tutorial_01";
    
    // Use this for initialization
	void Start ()
    {
        
        textBox = GetComponent<Text>();
        currentLine = 0;
        LineLog = currentLine;

        textIn(textName);
	}
	
	// Update is called once per frame
	void Update ()
    {
        //if (Input.GetKeyDown(KeyCode.Return))
        //{
        //    changeText();
        //}

		
	}

    void textIn(string title)
    {
        textOrigine = Resources.Load<TextAsset>("text/" + title);
        string[] serch = { "@@" };
        textLine = textOrigine.text.Split(serch, System.StringSplitOptions.RemoveEmptyEntries);
        changeText();

    }

    public bool changeText()
    {
        if (currentLine >= textLine.Length) return true;

        string s_Text = textLine[currentLine];
        textBox.text = s_Text;//.Substring(2);

        backLine = currentLine - LineLog;
        LineLog = currentLine;


        if (backLine < 0)
        {
            backLine = 0;
        }

        ++currentLine;
        if (currentLine >= textLine.Length)
        {
            //currentLine = 0;
            return true;
        }

        Debug.Log("次回：" + currentLine + "表示：" + LineLog + "戻る：" + backLine);
        return false;

    }

}
