using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal enum VibrateType
{
    VERTICAL,
    HORIZONTAL,
    BOTH
}

public class NormalItem : MonoBehaviour {

    [SerializeField] private VibrateType vibrateType;          //振動タイプ
    [Range(0, 1)] [SerializeField] private float vibrateRange; //振動幅
    [SerializeField] private float vibrateSpeed;               //振動速度

    private Vector3 initPosition;   //初期ポジション
    private Vector3 newPosition;    //新規ポジション
    private Vector3 minPosition;    //ポジションの下限
    private Vector3 maxPosition;    //ポジションの上限
    private bool[] directionToggle = new bool[3]; //振動方向の切り替え用トグル(オフ：値が小さくなる方向へ オン：値が大きくなる方向へ)

    // Use this for initialization
    void Start ()
    {
        switch (this.vibrateType)
        {
            case VibrateType.VERTICAL:
                this.initPosition.y = transform.localPosition.y;
                break;
            case VibrateType.HORIZONTAL:
                this.initPosition.x = transform.localPosition.x;
                break;
            case VibrateType.BOTH:
                this.initPosition.y = transform.localPosition.y;
                this.initPosition.x = transform.localPosition.x;
                break;
        }

        this.newPosition = this.initPosition;
        this.minPosition.x = this.initPosition.x - this.vibrateRange;
        this.minPosition.y = this.initPosition.y - this.vibrateRange;
        this.maxPosition.x = this.initPosition.x + this.vibrateRange;
        this.maxPosition.y = this.initPosition.y + this.vibrateRange;

        for(int i=0;i<directionToggle.Length;++i)
            this.directionToggle[i] = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Shake();
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
                this.transform.localPosition = new Vector3(0, this.newPosition.y, 0);
                break;
            case VibrateType.HORIZONTAL:
                this.transform.localPosition = new Vector3(this.newPosition.x, 0, 0);
                break;
            case VibrateType.BOTH:
                this.transform.localPosition = this.newPosition;
                break;
        }
    }

    public void Vibrate()
    {
        //ポジションが振動幅の範囲を超えた場合、振動方向を切り替える

        if (vibrateType == VibrateType.BOTH || vibrateType == VibrateType.VERTICAL)
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

        if (vibrateType == VibrateType.BOTH || vibrateType == VibrateType.HORIZONTAL)
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

        
    }
}

