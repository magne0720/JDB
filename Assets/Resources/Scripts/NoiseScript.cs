using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseScript : MonoBehaviour {

    private Material material;

	// Use this for initialization
	void Start () {
        this.material = this.GetComponent<Renderer>().material;
    }
	
	// Update is called once per frame
	void Update () {
        
        material.SetFloat("_T", Time.time);
	}
}
