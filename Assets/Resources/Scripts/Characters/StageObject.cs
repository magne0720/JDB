using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ステージに存在するものの基底
public class StageObject : MonoBehaviour {

    protected NavMeshAgentControlBehaviour agent;
    public Transform goal;

	// Use this for initialization
	void Start () {
        goal = transform.transform;
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 target = new Vector3();
        if (Input.GetKey(KeyCode.G))
        {
            target.x -= 0.01f;
        }
        if (Input.GetKey(KeyCode.Y))
        {
            target.z += 0.01f;
        }
        if (Input.GetKey(KeyCode.J))
        {
            target.x += 0.01f;
        }
        if (Input.GetKey(KeyCode.H))
        {
            target.z -= 0.01f;
        }
        goal.position.Set(target.x,target.y,target.z);
        agent.destination = goal;
    }
}
