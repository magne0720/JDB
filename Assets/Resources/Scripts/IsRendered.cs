using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsRendered : MonoBehaviour
{

    //メインカメラに付いているタグ名
    private const string MAIN_CAMERA_TAG_NAME = "PhoneCamera";

    //カメラに表示されているか
    private bool _isRendered = false;

    private void Update()
    {

        if (_isRendered)
        {
            Debug.Log("カメラに映ってるよ！");
        }

        _isRendered = false;
    }

    //カメラに映ってる間に呼ばれる
    private void OnWillRenderObject()
    {
        //メインカメラに映った時だけ_isRenderedを有効に
        if (Camera.current.tag == MAIN_CAMERA_TAG_NAME)
        {
            _isRendered = true;
        }
    }
}