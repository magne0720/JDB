using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeAdjust : IsRendered {


    float timer;
    public float distance;
    public AudioSource audio;

    // Use this for initialization
    void Start()
    {
        timer = 0;
        GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, 0, 1));

        transform.Rotate(new Vector3(0, distance, 0));

        if (audio.volume < 0.2f) audio.volume = 0.2f;
        else if (audio.volume > 0.25f) audio.volume = 0.25f;

        if (isRendered)
        {
            audio.volume += 0.001f;
        }
        else
        {
            audio.volume -= 0.001f;
        }
    }
}
