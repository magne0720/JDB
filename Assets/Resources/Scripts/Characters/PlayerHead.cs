using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHead : MonoBehaviour {
    Ray ray;
    RaycastHit hit;
    public Vector3 target;

    // Use this for initialization
    void Start()
    {
        ray = new Ray(transform.position, transform.forward);
    }

    // Update is called once per frame
    void Update()
    {
        ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit, 100.0f)) 
        {
            Debug.Log("Ray1" + hit.transform.position);
            target = hit.point;
        }
    }
    public Vector3 GetTargetPosition()
    {
        return target;
    }
}
