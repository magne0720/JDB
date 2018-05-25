using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderAppend : MonoBehaviour {

    private const string defName = "_T";
    private Material material;
    public string matName;

    // Use this for initialization
    void Start()
    {
        this.material = this.GetComponent<Renderer>().material;

        if (matName != null)
        {
            matName = defName;
        }
    }

    // Update is called once per frame
    void Update()
    {

        material.SetFloat(matName, Time.time);
    }
}
