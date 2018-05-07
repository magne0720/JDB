using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ホラーアイテム
public class HorrorItem : IsRendered {

    public bool isCaption;//1度写真に写ったかどうか

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(isCaption)
        transform.Rotate(Vector3.up);
	}

    public override void Caption()
    {
        Debug.Log(gameObject.name+",Caption!!!");
        isCaption = true;
        //Destroy(this);
    }
}
