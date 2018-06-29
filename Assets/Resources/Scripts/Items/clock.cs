using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clock : MonoBehaviour {

    public float timer;
    public AudioSource audio;

	// Use this for initialization
	void Start () {
        timer = 0;
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if (timer >= 60)
        {
            audio.Play();
            timer = 0;
        }
	}
}
