using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
//見えない敵クラス
public class EnemyControl : IsRendered {

    protected NavMeshAgent agent;
    public List<Vector3> points;
    public Transform player;
    public int destPoint;

    public bool isSpawn;    //事実上ステージに存在しているか
    public bool isNext;     //指定地点に届き、次の地点に迎える状態か
    public float NextTimer; //次の地点の指定する時間経過
    public const float LimitTime = 3.0f;//次の地点を指定するまでにかかる時間

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Use this for initialization
    void Start()
    {
        destPoint = 0;
        player = null;

        //agent.destination = points[destPoint];

        //agent.autoBraking = false;

        isNext = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            isNext = true;
        }

        if (isNext)
        {
            NextTimer += Time.deltaTime;
            if (NextTimer >= LimitTime)
            {
                GotoNextPoint();
                isNext = false;
                NextTimer = 0.0f;
            }
        }

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
    public override bool Caption()
    {
        //Destroy(gameObject);
        Debug.Log("Get");

        isSpawn = false;



        return true;
    }
    public void Respawn()
    {
        destPoint = 0;
        agent.destination = points[destPoint];
        isSpawn = true;
    }
}
