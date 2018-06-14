using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBeat : MonoBehaviour {
    AudioSource audio;

    //[Header("距離を測る敵のタグ名")]
    //public string Enemy_tag = null;
    //public GameObject Enemy;
    [Header("心音のオンオフ")]
    public bool OnHeartBeat = false;
    float distance;
    float timer;
    //public float BeatRange;
    [Range (20,60)]
    public float BeatSpeed;
    // Use this for initialization
    void Start () {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
            timer += Time.deltaTime;
        //if (distance < BeatRange)
        //{
        //    if (distance < BeatRange / 2) BeatSpeed = BeatSpeed = 45;
        //    if (distance < BeatRange / 2) BeatSpeed = BeatSpeed = 45;
        if (OnHeartBeat)
        {
            if (timer >= BeatSpeed / 60)
            {
                audio.Play();
                timer = 0;
            }
        }
        //}
		
	}

    //void distance_calc()
    //{
    //    distance = Vector3.Distance(this.transform.position, Enemy.transform.position);
    //}
}
