using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public static List<GameObject> SpawnPoints;
    public List<GameObject> SpawnEnemys;
    public GameObject SpawnObject;

    // Use this for initialization
    void Start()
    {
        SpawnPoints = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject g = transform.GetChild(i).gameObject;
            SpawnPoints.Add(g);
        }
        OriginalSpawn(1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OriginalSpawn(int num)
    {
        if (SpawnObject == null)
        {
            Debug.Log("SpawnObject NULL!");
            return;
        }
        for (int i = 0; i < num; i++)
        {
            GameObject g = Instantiate(SpawnObject);
            SpawnEnemys.Add(g);
            Result.Addtarget(g);
        }
    }
    public void Spawn()
    {
        foreach (GameObject g in SpawnEnemys)
        {
            EnemyControl ec = g.GetComponent<EnemyControl>();
            if (!ec.isSpawn)
            {
                int rand = Random.Range(0, SpawnPoints.Count);
                ec.points = SpawnPoints[rand].GetComponent<SpawnRoot>().GetRoot();
                ec.Respawn();
                g.transform.position = SpawnPoints[rand].transform.position;
            }
        }
    }
    public static List<Vector3> GetSpawnPoints(out Vector3 startPos,int num = -1)
    {
        int n = 0;
        List<Vector3> vecs = new List<Vector3>();

        if (num != -1)
        {
            n = num;
        }
        else
        {
            n = Random.Range(0, SpawnPoints.Count);
        }
        vecs = SpawnPoints[n].GetComponent<SpawnRoot>().roots;
        startPos = SpawnPoints[n].transform.position;

        return vecs;
    }
}
