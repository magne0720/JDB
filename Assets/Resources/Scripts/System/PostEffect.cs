//Shader用スクリプト

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostEffect : MonoBehaviour {

    public Material Damage;

    public float Depth = 1.2f;
    public float damage = -0.2f;


    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, Damage);
        Damage.SetFloat("_T", Time.time);
        Damage.SetFloat("_Depth", Depth);
        Damage.SetFloat("_Limit", damage);

    }

    void setDepth(float d)//Depthをprivateで使うとき用
    {
        Depth = d;
    }
}
