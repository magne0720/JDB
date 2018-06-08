using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーの手
public class PlayerHnad : MonoBehaviour {

    public GameObject child;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "HorrorItem")
        {
            c.transform.parent = transform;
            child = c.gameObject;
        }
    }
    //手を放す（子オブジェクトの解放）
    public void HandFree()
    {
        child.transform.parent = null;
        child = null;
    }
}
