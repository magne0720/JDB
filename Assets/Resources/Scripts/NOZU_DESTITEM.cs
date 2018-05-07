using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOZU_DESTITEM : MonoBehaviour {

    float timer;

	// Use this for initialization
	void Start () {
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        transform.Translate(new Vector3(0, 0, 1));
        if (timer > 3.0f)
        {
            Destroy(gameObject);
        }
	}
}
