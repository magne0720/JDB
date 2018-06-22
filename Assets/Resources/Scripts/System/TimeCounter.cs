using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCounter : MonoBehaviour {
    [Header ("制限時間(秒)")]
    public float TimeLimit = 90;
    public GameObject Light;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        TimeLimit -= Time.deltaTime;

        //Light.transform.Rotate(new Vector3(-Time.deltaTime,0,0));
    }
}
