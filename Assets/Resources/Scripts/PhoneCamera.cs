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
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("ScreenShot");
            TakePhoto();
            TakeTimes++;
        }
    }

    public void TakePhoto()
    {
        if (TakeTimes >= TAKE_LIMIT) return;

        Texture2D tex = new Texture2D(RenderTextureRef.width, RenderTextureRef.height, TextureFormat.RGB24, false);
        RenderTexture.active = RenderTextureRef;
        tex.ReadPixels(new Rect(0, 0, RenderTextureRef.width, RenderTextureRef.height), 0, 0);
        tex.Apply();


        texs.Add(tex);


        //// Encode texture into PNG
        //byte[] bytes = tex.EncodeToPNG();
        //Object.Destroy(tex);

        ////Write to a file in the project folder
        ////File.WriteAllBytes(Application.dataPath + "/../SavedScreen.png", bytes);
        //File.WriteAllBytes("C:/Users/student/Desktop/SavedScreen.png", bytes);
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