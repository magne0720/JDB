using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostEffect_SmartPhone : MonoBehaviour
{

    public Material IsoNoise;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, IsoNoise);
        IsoNoise.SetFloat("_T", Time.time);
    }
}
