using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{


    public static GameObject gCanvas;
    public static GameObject imageObj;
    public static List<GameObject> Gallery;
    public static List<GameObject> CameraPhotoTargets;
    public const float CaptionDis = 10.0f;//写真を撮った時に被写体として受け入れる距離

    // Use this for initialization
    void Start()
    {
        gCanvas = GameObject.Find("Content");
        imageObj = Resources.Load("Prefabs/ImageObj") as GameObject;

        if (gCanvas == null)
        {
            Debug.Log("Not gCanvas");
        }
        if (imageObj == null)
        {
            Debug.Log("Not imageObj");
        }
        Gallery = new List<GameObject>();
        CameraPhotoTargets = new List<GameObject>();
        //foreach (GameObject g in GameObject.FindGameObjectsWithTag("HorrorItem"))
        //{
        //    CameraPhotoTargets.Add(g);
        //    Debug.Log("<color=green>" + g.name + "</color>");
        //}
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.O))
        {
            InstGallery();
        }
    }

    //ギャラリー画面の作成
    public static void InstGallery()
    {
        if (Gallery != null)
        {
            foreach (GameObject g in Gallery)
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
            obj.transform.position = new Vector2(count * 165 % 660 + 100, obj.transform.parent.position.y - (count / 4 * (float)100) - 70);
            obj.transform.localScale = new Vector3(1, 1, 1);
            count++;
            Gallery.Add(obj);
        }
    }
    public static void GhostCheck(Vector3 camPos)
    {
        if (CameraPhotoTargets.Count > 0)
            Debug.Log("count"+CameraPhotoTargets.Count);
            foreach (GameObject g in CameraPhotoTargets)
            {
                if (Vector3.Distance(camPos, g.transform.position) < CaptionDis)
                {
                IsRendered IR = g.GetComponent<IsRendered>();
                g.transform.Rotate(new Vector3(0, 10, 0));
                    if (IR.isRendered)
                    {
                        IR.Caption();
                    }
                }
            }
    }
    public static void Addtarget(GameObject g)
    {
        CameraPhotoTargets.Add(g);
        Debug.Log("add"+g.name);
    }
}
