using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public static List<GameObject> LastPoints;
    public static List<GameObject> SpawnPoints;
    public List<GameObject> SpawnEnemys;
    public GameObject SpawnObject;
    public GameObject LastPointParent;

    public int putCount = 0;

    // Use this for initialization
    void Start()
    {
        SpawnPoints = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject g = transform.GetChild(i).gameObject;
            SpawnPoints.Add(g);
        }
        LastPoints = new List<GameObject>();
        for(int i=0;i<LastPointParent.transform.childCount;i++)
        {
            GameObject g = LastPointParent.transform.GetChild(i).gameObject;
            LastPoints.Add(g);
        }
        putCount = 0;
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
            GameObject g = Instantiate(SpawnObject,new Vector3(0,-25,0),Quaternion.identity);
            SpawnEnemys.Add(g);
            Result.Addtarget(g);
        }
    }
    public void Spawn(int num)
    {
        int limit = putCount + num;
        if(putCount<SpawnEnemys.Count)
        for (int i = putCount; i < limit; i++)
        {
            EnemyControl ec = SpawnEnemys[i].GetComponent<EnemyControl>();
            if (!ec.isSpawn)
            {
                int rand = Random.Range(0, SpawnPoints.Count);
                ec.points = SpawnPoints[rand].GetComponent<SpawnRoot>().GetRoot();
                ec.Respawn();
                putCount++;
            }
        }
    }
    public static List<Vector3> GetSpawnPoints(out Vector3 startPos, int num = -1)
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
    //最終地点をランダムで選択する
    public static Vector3 GetSpawnLastPoint(out Transform t)
    {
        Transform temp;
        int n = Random.Range(0, LastPoints.Count);

        temp = LastPoints[n].transform;

        t = temp;

        return temp.position;
    }
}
