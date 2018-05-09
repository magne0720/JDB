using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ホラーアイテム
public class HorrorItem : IsRendered {

    public bool isCaption;//1度写真に写ったかどうか


    private float Debugtimer;
	// Use this for initialization
	void Start () {
        isCaption = false;
        Debugtimer = 0;
	}

    // Update is called once per frame
    void Update()
    {
        if (isCaption)
        {
            Debugtimer += Time.deltaTime;
            transform.Rotate(Vector3.up);
        }
        if (Debugtimer >= 3.0f)
        {
            Debugtimer = 0;
            isCaption = false;
        }
        base.Update();
    }

    public override void Caption()
    {
        Debug.Log(gameObject.name+",<color=red>Caption!!!</color>");
        isCaption = true;
        //Destroy(this);
    }
}
