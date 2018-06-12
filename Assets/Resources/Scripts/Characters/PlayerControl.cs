using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour {

    protected CharacterController Controller = null;

    public PhoneCamera phone;//自撮り棒の先に置かれたカメラ
    public GameObject stick;//自撮り棒
    public GameObject head;//頭
    public GameObject leftHand;//左手
    public GameObject rightHand;//右手


    public float HP;

    public Vector3 forward;//ユーザーの胸板の方向
    public float dis;

    public bool isVRMode;
    public bool isStickMode;

    public static bool menu_active;
    // Use this for initialization
    void Start () {

        forward = transform.forward;

        HP = 100;

        if (isVRMode)
        {
            isStickMode = false;
        }

        menu_active = false;
    }

    // Update is called once per frame
    void Update()
    {
        //HPなくなったら
        if (HP <= 0)
        {
            HP = 0;
            transform.Translate(new Vector3(0, 1, 0));
        }
        InputControl();
        //InputRightPosition();
        if (!isVRMode)
        {
            InputCameraMoment(Input.GetAxis("CameraX"), Input.GetAxis("CameraY"));
            InputKeyboard();
            head.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
      
    }
    void Move(Vector2 target, bool isDash = false)
    {
        float speed = 2;
        if (isDash)
        {
            speed *= 3.0f;
        }
        if (target.y < 0)
        {
            target.x *= -1;
        }
        if (target.y > -0.2f && target.y < 0.2f)
        {
            target.y = 0;
        }
        transform.Translate(0,0,target.y*speed*Time.deltaTime);
        //forward = getDirectionDegree(forward, target.x / 2, 1);
        //transform.LookAt(new Vector3(forward.x-transform.position.x, transform.position.y, forward.z-transform.position.z));
    }

    void InputControl()
    {
        bool dash = false;

        if (OVRInput.GetDown(OVRInput.RawButton.A))
        {
            Debug.Log("Aボタンを押した");
            phone.TakePhoto();
        }
        if (OVRInput.Get(OVRInput.RawButton.A))
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
            transform.position=rightHand.GetComponent<RightHand>().GetTargetPosition();
            Debug.Log("右中指トリガーを押した");
        }
        if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
        {
            Debug.Log("左人差し指トリガーを押した");
            dash = true;
        }
        if (OVRInput.GetDown(OVRInput.RawButton.LHandTrigger))
        {
            dash = true;
            Debug.Log("左中指トリガーを押した");
        }
        //スティック
        // 左手のアナログスティックの向きを取得
        Vector2 stickL = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);
        //移動
        Move(stickL,dash);
        
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
        Vector2 vector = new Vector3();
        bool dash = false;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            dash = true;



        if (Input.GetKey(KeyCode.W))
        {
            vector.y += 1.0f;
            //transform.Translate(new Vector3(0, 0, Time.deltaTime * speed));
        }
        if (Input.GetKey(KeyCode.S))
        {
            //transform.Translate(new Vector3(0, 0, -Time.deltaTime * speed));
            vector.y -= 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            vector.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            vector.x += 1;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            phone.TakePhoto();
        }
        if (Input.GetMouseButtonDown(0))
        {
            phone.TakePhoto();
        }
        if (Input.GetMouseButton(0))
        {
            phone.TakePhoto();
        }
        if (Input.GetMouseButton(1))
        {
            InputCameraMoment(Input.GetAxis("CameraX"), Input.GetAxis("CameraY"));
        }
        //スティックを動かすモード
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isStickMode) isStickMode = false;
            else isStickMode = true;
        }

        if (!menu_active && (Input.GetKeyDown(KeyCode.Escape) || OVRInput.GetDown(OVRInput.RawButton.Back)))
        {
            OpenMenu();
        }

        float mad = Input.GetAxis("Mouse ScrollWheel");
        dis += mad;
        stick.transform.position = head.transform.forward*dis +transform.position;

        Move(vector, dash);
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
    void OpenMenu()
    {
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Additive);
        menu_active = true;

    }
    public static void setMenuActive(bool b)
    {
        menu_active = b;
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
    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Horror")
        {
            HP -= 1;
        }
    }
    void OnCollisionStay(Collision c)
    {
        if (c.gameObject.tag == "Horror")
        {
            HP -= 1;
        }
    }
}
