using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ホラーアイテム
public class HorrorItem : IsRendered {

    public bool isCaption;//1度写真に写ったかどうか
    public Vector3 OriginPos;//生成された時の場所

    private float Debugtimer;
	// Use this for initialization
	void Start () {
        isCaption = false;
        Debugtimer = 0;
        OriginPos = transform.position;
        GetComponent<Rigidbody>().freezeRotation = true;
        GetComponent<Rigidbody>().useGravity = true;
    }

    // Update is called once per frame
    void Update()
    {
        OriginPos.y = transform.position.y;
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
        if (Vector3.Distance(transform.position, OriginPos) > 4.0f)
        {
            
            transform.parent = null;
            Debug.Log("null Node");
        }
        if (Vector3.Distance(transform.position, OriginPos) > 0.2f)
        {
            if (transform.parent == null)
            {
                transform.LookAt(OriginPos);
                transform.Translate(new Vector3(0, 0, Time.deltaTime));
                //transform.Translate((transform.position - OriginPos).normalized * Time.deltaTime);
            }
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
