using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//生成された写真を縮小し、最終的に削除する

public class PictureFade : MonoBehaviour {

    public float DestTime = 3.0f;
    public float timer;
    RawImage raw;

	// Use this for initialization
	void Start () {
        raw = GetComponent<RawImage>();

        timer = DestTime;
        raw.rectTransform.anchoredPosition = new Vector2(0.2f, 0.2f);
    }
	
	// Update is called once per frame
	void Update () {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            raw.color = new Color(255, 255, 255, timer/DestTime);
            transform.transform.localScale *= (timer / DestTime);
        }
        else
        {
            Destroy(gameObject);
        }
	}
    public void SetTimer(float t)
    {
        DestTime = t;
    }
}
