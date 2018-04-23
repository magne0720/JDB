using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    PhoneCamera phone;

    public GameObject testObj;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        InputControl();
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
            Instantiate(testObj);
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
    }
}
