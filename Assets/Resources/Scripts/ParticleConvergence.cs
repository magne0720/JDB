using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleConvergence : MonoBehaviour {
    public ParticleSystem pSystem;
    float f;
	// Use this for initialization
	void Start () {
        pSystem = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        f = pSystem.time;
        Debug.Log(f);
        if(f > 2.5)
        {
            //pSystem.startSpeed = -pSystem.startSpeed;
        }
	}
}
