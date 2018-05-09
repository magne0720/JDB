using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PhoneCamera : MonoBehaviour
{
    public const int TAKE_LIMIT = 36;
    public int TakeTimes = 0;

    public Camera cam;
    public RenderTexture RenderTextureRef;
    
    public static List<Texture2D> texs;

    // Use this for initialization
    void Start()
    {
        texs = new List<Texture2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward*1000,Color.red,0.4f);
        Debug.DrawRay(transform.position, -transform.forward * 5.0f);
    }
    void OnDrawGizmos()
    {

        RaycastHit hit;
        bool isHit = Physics.SphereCast(transform.position, 0.3f, transform.forward * 10,out hit);
        if (isHit)
        {
            Gizmos.DrawRay(transform.position, transform.forward * hit.distance);
            Gizmos.DrawWireSphere(transform.position + transform.forward * (hit.distance), 0.3f);
        }
        else
        {
            Gizmos.DrawRay(transform.position, transform.forward * 100);
        }
    }
    public void TakePhoto()
    {
        //if (TakeTimes >= TAKE_LIMIT) return;


        Texture2D tex = new Texture2D(RenderTextureRef.width, RenderTextureRef.height, TextureFormat.RGB24, false);
        RenderTexture.active = RenderTextureRef;
        tex.ReadPixels(new Rect(0, 0, RenderTextureRef.width, RenderTextureRef.height), 0, 0);
        tex.Apply();


        texs.Add(tex);

        TakeTimes++;

        GhostCheck(transform.position);

        //// Encode texture into PNG
        //byte[] bytes = tex.EncodeToPNG();
        //Object.Destroy(tex);

        ////Write to a file in the project folder
        ////File.WriteAllBytes(Application.dataPath + "/../SavedScreen.png", bytes);
        //File.WriteAllBytes("C:/Users/student/Desktop/SavedScreen.png", bytes);
    }
    public void GhostCheck(Vector3 myPos)
    {
        Result.GhostCheck(myPos);
    }
    public static List<Texture2D> GetTextures()
    {
        return texs;
    }
    public static int GetTotalPhotoNumber()
    {
        return texs.Count;
    }
}