using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour {

    public GameObject gCanvas;
    public GameObject imageObj;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("CameraRoll");
            CameraRoll();
        }
	}

    public void CameraRoll()
    {
        int count=0;

        foreach (Texture2D t in PhoneCamera.GetTextures())
        {
            GameObject obj=Instantiate(imageObj);
            obj.GetComponentInChildren<RawImage>().texture=t;
            obj.transform.parent = gCanvas.transform;
            obj.transform.position = new Vector2(count*165%660+100, Screen.height-(count / 4*(float)100)-70);
            obj.transform.localScale = new Vector3(1,1,1);
            count++;
        }
    }
}
