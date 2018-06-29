using System.Collections;
using System.Collections.Generic;
using UnityEngine;


internal enum VibrateType
{
    VERTICAL,
    HORIZONTAL,
    DEPTH,
    ALL
}

[RequireComponent(typeof(Rigidbody))]
public class NormalItem : MonoBehaviour {

    [SerializeField] private VibrateType vibrateType;          //振動タイプ
    [Range(0, 1)] [SerializeField] private float vibrateRange; //振動幅
    [SerializeField] private float vibrateSpeed;               //振動速度

    private Vector3 initPosition;   //初期ポジション
    private Vector3 newPosition;    //新規ポジション
    private Vector3 minPosition;    //ポジションの下限
    private Vector3 maxPosition;    //ポジションの上限
    private bool[] directionToggle = new bool[3]; //振動方向の切り替え用トグル(オフ：値が小さくなる方向へ オン：値が大きくなる方向へ)

    //player接近判定用
    //player取得
    //private string[] reactionObjectName;
    private List<GameObject> reactionObject = new List<GameObject>();
    private float caputureRange;

    // Use this for initialization
    void Start()
    {
        //test用記述
        vibrateType = VibrateType.ALL;
        vibrateRange = 0.05f;
        vibrateSpeed = 3;
        caputureRange = 2;
        //playerName = "player";
        //test用記述

        this.initPosition.y = transform.localPosition.y;
        this.initPosition.x = transform.localPosition.x;
        this.initPosition.z = transform.localPosition.z;

        this.newPosition = this.initPosition;
        this.minPosition.x = this.initPosition.x - this.vibrateRange;
        this.minPosition.y = this.initPosition.y - this.vibrateRange;
        this.minPosition.z = this.initPosition.z - this.vibrateRange;

        this.maxPosition.x = this.initPosition.x + this.vibrateRange;
        this.maxPosition.y = this.initPosition.y + this.vibrateRange;
        this.maxPosition.z = this.initPosition.z + this.vibrateRange;

        for (int i = 0; i < directionToggle.Length; ++i)
            this.directionToggle[i] = false;

        //for (int i = 0; i < reactionObjectName.Length; ++i)
        //{
        //    //Debug.Log(playerName[i]);
        //    reactionObject.Add(GameObject.Find(reactionObjectName[i]));
        //}

        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (checkDrawNearer())
        {
            //Constraintsで振動中は回転とPositionが変わらない
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            Shake();
        }
        else
        {
            //Constraintsを解除
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            resetPosition();
        }
        //List<GameObject> objs = Result.CameraPhotoTargets;
	}
    //自身を揺らす
    public void Shake()
    {
        Vibrate();
        //新規ポジションを代入
        switch (this.vibrateType)
        {
            case VibrateType.VERTICAL:
                this.transform.localPosition = new Vector3(initPosition.x, this.newPosition.y, initPosition.z);
                break;
            case VibrateType.HORIZONTAL:
                this.transform.localPosition = new Vector3(this.newPosition.x, initPosition.y, initPosition.z);
                break;
            case VibrateType.DEPTH:
                this.transform.localPosition = new Vector3(this.newPosition.x, initPosition.y, initPosition.z);
                break;
            case VibrateType.ALL:
                this.transform.localPosition = this.newPosition;
                break;
        }
    }

    public void Vibrate()
    {
        //ポジションが振動幅の範囲を超えた場合、振動方向を切り替える

        if (vibrateType == VibrateType.ALL| vibrateType == VibrateType.VERTICAL)
        {
            if (this.newPosition.y <= this.minPosition.y ||
                this.maxPosition.y <= this.newPosition.y)
            {
                this.directionToggle[1] = !this.directionToggle[1];
            }
            //新規ポジションを設定
            this.newPosition.y = this.directionToggle[1] ?
                this.newPosition.y + (vibrateSpeed * Time.deltaTime) :
                this.newPosition.y - (vibrateSpeed * Time.deltaTime);
            this.newPosition.y = Mathf.Clamp(this.newPosition.y, this.minPosition.y, this.maxPosition.y);

        }

        if (vibrateType == VibrateType.ALL || vibrateType == VibrateType.HORIZONTAL)
        {
            if (this.newPosition.x <= this.minPosition.x ||
                this.maxPosition.x <= this.newPosition.x)
            {
                this.directionToggle[0] = !this.directionToggle[0];
            }
            //新規ポジションを設定
            this.newPosition.x = this.directionToggle[0] ?
                this.newPosition.x + (vibrateSpeed * Time.deltaTime) :
                this.newPosition.x - (vibrateSpeed * Time.deltaTime);
            this.newPosition.x = Mathf.Clamp(this.newPosition.x, this.minPosition.x, this.maxPosition.x);
        }

        if (vibrateType == VibrateType.ALL || vibrateType == VibrateType.DEPTH)
        {
            if (this.newPosition.z <= this.minPosition.z ||
                this.maxPosition.z <= this.newPosition.z)
            {
                this.directionToggle[2] = !this.directionToggle[2];
            }
            //新規ポジションを設定
            this.newPosition.z = this.directionToggle[2] ?
                this.newPosition.z + (vibrateSpeed * Time.deltaTime) :
                this.newPosition.z - (vibrateSpeed * Time.deltaTime);
            this.newPosition.z = Mathf.Clamp(this.newPosition.z, this.minPosition.z, this.maxPosition.z);
        }


    }

    bool checkDrawNearer()
    {
        for (int i = 0; i < reactionObject.Count; ++i)
        {
            //Debug.Log(player[i].transform.position.x + ":" + (initPosition.x - caputureRange));
            if (reactionObject[i].transform.position.x >= initPosition.x - caputureRange
                && reactionObject[i].transform.position.x <= initPosition.x + caputureRange
                && reactionObject[i].transform.position.z >= initPosition.z - caputureRange
                && reactionObject[i].transform.position.z <= initPosition.z + caputureRange
                && reactionObject[i].transform.position.y >= initPosition.y - caputureRange
                && reactionObject[i].transform.position.y <= initPosition.y + caputureRange)
            {
                //Debug.Log("今年は何年だぁ！？");
                return true;
            }
        }

        //Debug.Log("ウッキー！　今年はサル年ィ！！");
        return false;
    }

    public void addReactionObject(GameObject obj)
    {
        reactionObject.Add(obj);
    }

    void resetPosition()
    {
        if (this.transform.position.x != initPosition.x)
        {
            initPosition.x = this.transform.position.x;
            this.minPosition.x = this.initPosition.x - this.vibrateRange;
            this.maxPosition.x = this.initPosition.x + this.vibrateRange;
        }
        if (this.transform.position.y != initPosition.y)
        {
            initPosition.y = this.transform.position.y;
            this.minPosition.y = this.initPosition.y - this.vibrateRange;
            this.maxPosition.y = this.initPosition.y + this.vibrateRange;
        }
        if (this.transform.position.z != initPosition.z)
        {
            initPosition.z = this.transform.position.z;
            this.minPosition.z = this.initPosition.z - this.vibrateRange;
            this.minPosition.z = this.initPosition.z - this.vibrateRange;
        }

        this.newPosition = this.initPosition;
    }

    void awakeConstraints()
    {
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }


}

