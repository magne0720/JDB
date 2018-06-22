using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
//ステージに存在するものの基底
public class StageObject : MonoBehaviour {

    protected NavMeshAgent agent;
    public List<Vector3> points;
    public Transform player;
    public int destPoint;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Use this for initialization
    void Start () {
        destPoint = 0;
        player = null;

        agent.destination = points[destPoint];

        agent.autoBraking = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();


        //agent.isStopped = false;
    }


    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Count == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint];

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Count;
    }


}
