using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoot : MonoBehaviour {

    public List<Vector3> roots; 

	// Use this for initialization
	void Start () {
        roots = new List<Vector3>();
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject g = transform.GetChild(i).gameObject;
            roots.Add(g.transform.position);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public List<Vector3> GetRoot()
    {
        return roots;
    }
}
