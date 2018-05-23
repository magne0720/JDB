using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBeatScript : MonoBehaviour {
    AudioSource audio;
    float beat_timer = 0;
    [Header("HeartBeatParametor")]
    public float bpm = 90;

    [Header("AudioClip")]
    public AudioClip HeartBeatSound;
	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
        audio.clip = HeartBeatSound;
	}
	
	// Update is called once per frame
	void Update () {
        bpm =  Vector3.Distance(this.gameObject.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) * 5;
        if (bpm < 15) bpm = 15;
        beat_timer += Time.deltaTime;
        if(bpm < 90 && beat_timer > bpm/60)
        {
            audio.Play();
            beat_timer = 0;
        }
	}
}
