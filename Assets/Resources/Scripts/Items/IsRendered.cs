using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//カメラに写った状態が知りたいものの基底クラス
public class IsRendered : MonoBehaviour
{
    public PhoneCamera cam;

    //基本的にパーティクルを出す
    public ParticleSystem Particle;
    private ParticleSystem particleObj;

    //メインカメラに付いているタグ名
    private const string MAIN_CAMERA_TAG_NAME = "PhoneCamera";

    //カメラに表示されているか
    public bool isRendered = false;
    public bool isCaption = false;//一度撮られたか

    void Start()
    {
        Result.Addtarget(gameObject);
    }

    public virtual void Update()
    {
        if (isRendered&&cam)
        {

        }
    }
    void LateUpdate()
    {
        //写っている状態を遅らせるため
        isRendered = false;
    }

    //カメラに映ってる間に呼ばれる
    private void OnWillRenderObject()
    {
        //メインカメラに映った時だけisRenderedを有効に
        if (Camera.current.tag == MAIN_CAMERA_TAG_NAME)
        {
            cam = Camera.current.gameObject.GetComponentInParent<PhoneCamera>();
            isRendered = true;
        }

        //位置
    }
    public virtual bool Caption()
    {
        //継承先で変わる
        isCaption = true;

        return false;
    }
    public void InstParticle(int num)
    {
        if (Particle != null)
        {
            particleObj = Instantiate(Particle, transform.position, Quaternion.identity);
            particleObj.gameObject.layer = num;
            particleObj.Play();
        }
    }
    public void StopParticle()
    {
        particleObj.Stop();
    }
}