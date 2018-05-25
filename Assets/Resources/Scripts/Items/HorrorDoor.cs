using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//条件を満たしたら開くドア
public class HorrorDoor : MonoBehaviour {

    public HorrorItem linkItem;//同期させるアイテム
    public bool isOpen;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (linkItem.isCaption)
        {
            isOpen = true;
        }
        if (isOpen&&transform.position.y<10)
        {
            transform.Translate(new Vector3(0, 1, 0));
        }
	}
}
