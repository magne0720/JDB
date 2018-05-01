using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour {

    public GameObject player;

	// Use this for initialization
	void Start () {
        gameObject.layer = 9;
	}
	
	// Update is called once per frame
	void Update () {
        if (player != null)
        {
            transform.LookAt(player.transform.position);
        }
	}
    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.layer==8)
        {
            //自身のタグを変える
            gameObject.layer=10;
        }
    }
}
