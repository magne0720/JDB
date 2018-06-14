using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleRayScript : MonoBehaviour {
    Ray ray;
    Ray mouseRay;
    float distance = 10; // 飛ばす&表示するRayの長さ
    float duration = 0.1f;   // 表示期間（秒）
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ray = new Ray(transform.position, transform.forward);
        //mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);   // Use this for initialization
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.red, duration, false);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, distance))
        {
            if (hit.transform.tag == "TV") transitionScene();
            else Debug.Log("当たってない");
        }

    }

    void transitionScene()
    {
        //ここに処理を書く
        Debug.Log("当たってる");
    }
}
