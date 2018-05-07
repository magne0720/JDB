using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRote : MonoBehaviour {

    public GameObject obj;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W)) obj.transform.position += obj.transform.forward * 0.1f;
        if (Input.GetKey(KeyCode.S)) obj.transform.position -= obj.transform.forward * 0.1f;
        if (Input.GetKey(KeyCode.D)) obj.transform.Rotate(0,0,-1);
        if (Input.GetKey(KeyCode.A)) obj.transform.Rotate(0,0, 1);

        //--//

        Vector3 m = Vector3.zero;

        m.y = Input.GetAxis("Mouse X");
        m.x = Input.GetAxis("Mouse Y") * -1;

        //obj.transform.Rotate(obj.transform.right, m.x);
        //obj.transform.Rotate(obj.transform.up, m.y);

        obj.transform.eulerAngles += m;
    }
}
