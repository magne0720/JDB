using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//カメラに写った状態が知りたいものの基底クラス
public class IsRendered : MonoBehaviour
{

    //メインカメラに付いているタグ名
    private const string MAIN_CAMERA_TAG_NAME = "PhoneCamera";

    //カメラに表示されているか
    public bool isRendered = false;

    public virtual void Update()
    {
        //if (isRendered)
        //{
        //    Debug.Log(gameObject.name+"はカメラに映ってるよ！");
        //}
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
            isRendered = true;
        }

        //位置
        //Camera.current.
    }
    public virtual void Caption()
    {
        //継承先で変わる
    }
}