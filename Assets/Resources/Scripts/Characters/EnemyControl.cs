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
    public GameObject ExObj;//消滅時に発生させるパーティクル
    public int destPoint;
    public GameObject LastPointObj;

    public bool isSpawn;    //事実上ステージに存在しているか
    public bool isNext;     //指定地点に届き、次の地点に迎える状態か
    public bool isLastAttack;//最後の攻撃に来たか
    public float NextTimer; //次の地点の指定する時間経過
    public const float LimitTime = 3.0f;//次の地点を指定するまでにかかる時間

    Animator animator;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start()
    {
        destPoint = 0;
        player = null;
        //isNext = true;
        animator.SetBool("Walk", false);

        LastPointObj = GameObject.Find("EnemyLastPoint");
        //player = GameObject.Find("Player").transform;
        //agent.isStopped=true;
        isNext = false;
        isLastAttack = false;        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSpawn)
        {
            agent.isStopped = true;
        }
        if (agent.isStopped)
        {
            return;
        }

        if (!isLastAttack&&!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            isNext = true;
            SetLayerAllChildren(14);
            animator.SetBool("Walk", false);
        }

        if (isNext)
        {
            NextTimer += Time.deltaTime;
            if (NextTimer >= LimitTime)
            {
                if (destPoint >= points.Count-1)
                {
                    //ゲームオーバー寸前
                    LastRoot();
                    NextTimer = 0.0f;
                }
                else
                {
                    GotoNextPoint();
                    InstOrb();
                    isNext = false;
                    SetLayerAllChildren(9);
                    NextTimer = 0.0f;
                    animator.SetBool("Walk", true);
                }
            }

        }
        else
        {
            InstParticle(gameObject.layer);

        }

            //if (Input.GetKeyDown(KeyCode.Q)) Respawn();

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

    void InstOrb()
    {
        Instantiate(Particle,transform.position,Quaternion.identity);
    }
    public override bool Caption()
    {
        if(!isSpawn)
        {
            return false;
        }

        //Destroy(gameObject);
        Debug.Log("Get");

        isSpawn = false;

        Respawn();

        return true;
    }
    public void Respawn()
    {
        Damage();

        Vector3 v=new Vector3();
        points = SpawnManager.GetSpawnPoints(out v);
        agent.Warp(v);
        destPoint = 0;
        agent.destination = points[destPoint];
        isSpawn = true;
        isLastAttack = false;

        animator.SetBool("Attack", false);
        agent.isStopped = false;
    }

    void LastRoot()
    {
        if (isLastAttack)
        {
            Attack();
        }
        else
        {
            Transform t;

            SetLayerAllChildren(9);

            isLastAttack = true;
            //ベッドからのぞくアニメーション
            agent.Warp(SpawnManager.GetSpawnLastPoint(out t));
            transform.rotation = t.rotation;

            animator.SetBool("Attack", true);
        }
    }

    void Attack()
    {
        //この処理が通ったらゲームオーバーにする
        GameManager.GameOver();
    }

    void Damage()
    {
        if (ExObj != null)
        {
            Instantiate(ExObj, transform.position, Quaternion.identity);
        }
    }

    //子オブジェクトのレイヤーをすべて変更する
    void SetLayerAllChildren(int num)
    {
        foreach(Transform t in GetComponentsInChildren<Transform>())
        {
            t.gameObject.layer = num;
        }
    }
}
