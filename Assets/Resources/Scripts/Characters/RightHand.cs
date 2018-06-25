using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHand : MonoBehaviour {

    Ray ray;
    RaycastHit hit;
    public Vector3 target;

	// Use this for initialization
	void Start () {
        ray = new Ray(transform.position, transform.forward);
	}
	
	// Update is called once per frame
	void Update () {
        ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward);
        if (Physics.Raycast(ray,out hit, 5.0f))
        {
            //Debug.Log("Ray" + hit.transform.position);
            target = hit.transform.position;
        }
    }
    public Vector3 GetTargetPosition()
    {
        return target;
    }
}
