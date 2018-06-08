using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test_act_check : MonoBehaviour {

    List<Slider> test_s = new List<Slider>();
    string[] slider_names = { "Slider_test1", "Slider_test2" };
    int cursor;

	// Use this for initialization
	void Start ()
    {
        for (int i = 0; i < slider_names.Length; ++i)
        {
            test_s.Add(GameObject.Find(slider_names[i]).GetComponent<Slider>());
            Debug.Log(slider_names[i]);
            test_s[i].value = 2;
        }
        cursor = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            Debug.Log("enter");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("いまスペースキーが押された");
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            Debug.Log("Up");
            --cursor;
            if (cursor < 0) cursor = 0;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            ++cursor;
            if (cursor == slider_names.Length) --cursor;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            test_s[cursor].value--;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            test_s[cursor].value++;
        }
    }
}
