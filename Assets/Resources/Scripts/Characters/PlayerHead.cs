using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerHead : MonoBehaviour {
    Ray ray;
    RaycastHit hit;
    public Vector3 target;
    public GameObject targetObj;
    private NavMeshAgent agent;
    private NavMeshHit nHit;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent < NavMeshAgent> ();
        ray = new Ray(transform.position, transform.forward+new Vector3(0,-5,0));
    }

    // Update is called once per frame
    void Update()
    {
        //ray = new Ray(transform.position, transform.forward);
        ray = new Ray(transform.position, transform.forward + new Vector3(0, -0.5f, 0));
        Debug.DrawRay(transform.position, transform.forward + new Vector3(0, -0.5f, 0),Color.red);
        if (Physics.Raycast(ray, out hit, 100.0f)) 
        {
            if (!agent.Raycast(hit.point, out nHit))
            {
                target = hit.point;

                //targetObj.transform.position = target;
            }
        }
    }
    public Vector3 GetTargetPosition()
    {
        return target;
    }
}
