using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBeat : MonoBehaviour {
    AudioSource audio;
    SoundEffect SE;

    //[Header("距離を測る敵のタグ名")]
    //public string Enemy_tag = null;
    public GameObject Enemy;

    float distance;
    float timer;
    // Use this for initialization
    void Start () {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
            timer += Time.deltaTime;
        if (distance < 60)
        {
            if (distance < 20) distance = 20;
            if (timer >= distance / 60)
            {
                audio.Play();
                timer = 0;
            }
        }
		
	}

    void distance_calc()
    {
        distance = Vector3.Distance(this.transform.position, Enemy.transform.position);
    }
}
