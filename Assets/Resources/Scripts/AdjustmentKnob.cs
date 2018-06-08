using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AdjustmentKnob : MonoBehaviour {

    Slider m_slider;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		//ボタン入力
        //縦：カーソル　横：音量

	}

    public float getValue()
    {
        m_slider = gameObject.GetComponent<Slider>();
        return m_slider.value;
    }

    
}
