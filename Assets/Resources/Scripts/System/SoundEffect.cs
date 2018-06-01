using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour {

    [Header("SoundEffect")]
    [Space(12)]

    [Tooltip("歩く音")]
    public AudioClip Walk;
    [Space(8)]

    [Tooltip("走る音")]
    public AudioClip Dash;
    [Space(8)]

    [Tooltip("扉の開く音")]
    public AudioClip OpenDoor;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
