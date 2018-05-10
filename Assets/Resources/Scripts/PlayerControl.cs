using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public PhoneCamera phone;//自撮り棒の先に置かれたカメラ
    public GameObject stick;//自撮り棒
    public GameObject head;//頭
    public GameObject leftHand;//左手


    public Vector3 forward;
    public float dis;

    public bool isVRMode;
    public bool isStickMode;
    // Use this for initialization
    void Start () {
        
        if (isVRMode)
        {
            isStickMode = false;
        }
	}

    // Update is called once per frame
    void Update()
    {
        InputControl();
        //InputRightPosition();
        if (!isVRMode)
        {
            InputKeyboard();
            head.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            InputCameraMoment(Input.GetAxis("CameraX"), Input.GetAxis("CameraY"));
        }
    }
    void Move(Vector2 target)
    {
        transform.Translate(target.x * Time.deltaTime, 0, target.y * Time.deltaTime);
    }

    void InputControl()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.A))
        {
            Debug.Log("Aボタンを押した");
            phone.TakePhoto();
        }
        if (OVRInput.GetDown(OVRInput.RawButton.B))
        {
            Debug.Log("Bボタンを押した");
            Result.InstGallery();
        }
        if (OVRInput.GetDown(OVRInput.RawButton.X))
        {
            Debug.Log("Xボタンを押した");

        }
        if (OVRInput.GetDown(OVRInput.RawButton.Y))
        {
            Debug.Log("Yボタンを押した");
        }
        if (OVRInput.GetDown(OVRInput.RawButton.Start))
        {
            Debug.Log("メニューボタン（左アナログスティックの下にある）を押した");
        }
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
            Debug.Log("右人差し指トリガーを押した");
        }
        if (OVRInput.GetDown(OVRInput.RawButton.RHandTrigger))
        {
            Debug.Log("右中指トリガーを押した");
        }
        if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
        {
            Debug.Log("左人差し指トリガーを押した");
        }
        if (OVRInput.GetDown(OVRInput.RawButton.LHandTrigger))
        {
            Debug.Log("左中指トリガーを押した");
        }
        //スティック
        // 左手のアナログスティックの向きを取得
        Vector2 stickL = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);
        //移動
        Move(stickL);
        
        // 右手のアナログスティックの向きを取得
        Vector2 stickR = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick);
        

    }

    //右手コントローラーの位置の設定
    void InputRightPosition()
    {
        stick.transform.position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
    }
    //テスト用のキーボード操作
    void InputKeyboard()
    {
        if (!isStickMode)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(new Vector3(0, 0, Time.deltaTime));
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(new Vector3(0, 0, -Time.deltaTime));
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            phone.TakePhoto();
        }
        if (Input.GetMouseButtonDown(0))
        {
            phone.TakePhoto();
        }
        //スティックを動かすモード
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isStickMode) isStickMode = false;
            else isStickMode = true;
        }

        float mad = Input.GetAxis("Mouse ScrollWheel");
        dis += mad;
        stick.transform.position = head.transform.forward*dis +transform.position;
    }
    //左手に触れたアイテムを左手が持つ（タグ条件）
    void CatchLeftHand(string tagname)
    {
        Debug.Log("catch"+tagname);
    }
    void InputCameraMoment(float x, float y)
    {
        forward += new Vector3(x, y, 0);

        if (forward.y > 80) forward.y = 80;//下方向の上限
        if (forward.y < -80) forward.y = -80;//上方向の上限

        if (!isStickMode) {
            head.transform.rotation = Quaternion.Euler(new Vector3(forward.y, forward.x, 0));
            transform.rotation = Quaternion.Euler(new Vector3(0, forward.x, 0));
        }
        else
        {
            stick.transform.rotation = Quaternion.Euler(new Vector3(forward.y, forward.x, 0));
        }
    }
    Vector3 getDirectionDegree(Vector3 target,float deg,float range = 1.0f)
    {
        Vector3 vector = target.normalized;

        //ラジアンに変換
        float rag = deg + Mathf.PI / 180;

        float ax = vector.x * Mathf.Cos(rag) - vector.z * Mathf.Sin(rag);
        float az = vector.x * Mathf.Sin(rag) - vector.z * Mathf.Cos(rag);

        vector.x = ax * range;
        vector.z = az * range;

        return vector;
    }
    Vector3 RotateY(Vector3 target,float deg,float range = 1.0f)
    {
        Vector3 vector = (target).normalized;

        //ラジアンに変換
        float rag = deg + Mathf.PI / 180;
        
        float az = vector.z * Mathf.Cos(rag) - vector.x * Mathf.Sin(rag);
        float ax = vector.z * Mathf.Sin(rag) - vector.x * Mathf.Cos(rag);

        vector.z = az * range;
        vector.x = ax * range;

        return vector;
    }
}
