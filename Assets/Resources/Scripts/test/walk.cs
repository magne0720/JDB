using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walk : MonoBehaviour {
    AudioSource audio;
    SoundEffect SE;
    int WalkState = 0;
    bool walking;
	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
        SE = GameObject.Find("SEtest").GetComponent<SoundEffect>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                WalkState = 2;
            }
            else  WalkState = 1;
        }
        else { WalkState = 0; }

        Debug.Log(WalkState);
        OnSound();
        if (!audio.isPlaying)
        {
            audio.Play();
        }
    }

    void OnSound()
    {
        switch(WalkState)
        {
            case 0:audio.clip = null; break;
            case 1:if(audio.clip != SE.Walk) audio.clip = SE.Walk;
                break;
            case 2:if(audio.clip != SE.Dash)audio.clip = SE.Dash;
                break;

            default: break;
        }
    }
}
