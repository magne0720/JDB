﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walk : MonoBehaviour {
    AudioSource audio;
    public AudioClip Walk;
    public AudioClip Dash;
    int WalkState = 0;
    bool walking;
	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W) || OVRInput.Get(OVRInput.RawAxis2D.LThumbstick) != Vector2.zero)
        {
            if (Input.GetKey(KeyCode.LeftShift)|| Input.GetKey(KeyCode.RightShift) || OVRInput.Get(OVRInput.RawButton.LIndexTrigger))
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
            case 1:if(audio.clip != Walk) audio.clip = Walk;
                break;
            case 2:if(audio.clip != Dash)audio.clip = Dash;
                break;

            default: break;
        }
    }
}
