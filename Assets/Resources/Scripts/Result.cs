using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour {


    public static GameObject gCanvas;
    public static GameObject imageObj;
    public static List<GameObject> Gallery;

	// Use this for initialization
	void Start () {
        gCanvas = GameObject.Find("Content");
        imageObj = Resources.Load("Prefabs/ImageObj") as GameObject;

        if (gCanvas != null)
        {
            Debug.Log("Apperer gCanvas");
        }
        if (imageObj != null)
        {
            Debug.Log("Load imageObj");
        }
        Gallery = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        CameraRoll();

        if (Input.GetKeyDown(KeyCode.O))
        {
            InstGallery();
        }
	}
    public static void InstGallery()
    {
        if (Gallery != null)
        {
            foreach(GameObject g in Gallery)
            {
                Destroy(g);
            }
        }
        Gallery = new List<GameObject>();

        int count = 0;

        foreach (Texture2D t in PhoneCamera.GetTextures())
        {
            GameObject obj = Instantiate(imageObj);
            obj.GetComponentInChildren<RawImage>().texture = t;
            obj.transform.parent = gCanvas.transform;
            //obj.transform.position = new Vector2(count * 165 % 660 + 100, (count / 4 * (float)100) - 70);
            obj.transform.position = new Vector2(count * 165 % 660 + 100, obj.transform.parent.position.y- (count / 4 * (float)100) - 70);
            obj.transform.localScale = new Vector3(1,1,1);
            count++;
            Gallery.Add(obj);
        }
    }

    public void CameraRoll()
    {
       
        foreach (GameObject obj in Gallery)
        {
            
        }
    }
}
