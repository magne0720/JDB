using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test_act_check : MonoBehaviour {

    //画面上のスライダーの容れ物
    List<Slider> test_s = new List<Slider>();
    //スライダーの名前。変更したらこっちも変えて
    string[] slider_names = { "Slider_test1", "Slider_test2" };
    //カーソル位置用の変数
    int cursor;

	// Use this for initialization
	void Start ()
    {
        //名前に従ってスライダーを探す。
        for (int i = 0; i < slider_names.Length; ++i)
        {
            test_s.Add(GameObject.Find(slider_names[i]).GetComponent<Slider>());
            Debug.Log(slider_names[i]);
            //デフォルトで設定したい値があればここで放り込む。txtなり引っ張ってくるなりご随意に
            //test_s[i].value = 2;
        }
        cursor = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //エンターなりボタンを叩くと現在の音量を上から吐き出す。テキストなりstaticなりに入れてあげて。　2018_0611
        if (Input.GetKeyDown(KeyCode.Return))
        {
            for (int i = 0; i < test_s.Count; ++i)
            {
                Debug.Log(test_s[i].value);
            }
        }
        
        //上下ボタンでカーソル切り替え。音量調節の対象を切り替える。いらなければ削除　2018_0611
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Up");
            --cursor;
            if (cursor < 0) cursor = 0;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ++cursor;
            if (cursor == slider_names.Length) --cursor;
        }
        //左右ボタンでカーソルの対象となる音量を調節
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            test_s[cursor].value--;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            test_s[cursor].value++;
        }
    }
}
