using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ホラーアイテム
public class HorrorItem : IsRendered {

    public bool isCaption;//1度写真に写ったかどうか
    public Vector3 OriginPos;//生成された時の場所

    //特定のアクションを起こすとエフェクトを出す
    public ParticleSystem actionParticle;
    //それに応じたサウンド
    public AudioClip actionEffect;

    // Use this for initialization
    void Start () {
        isCaption = false;
        OriginPos = transform.position;
        GetComponent<Rigidbody>().freezeRotation = true;
        GetComponent<Rigidbody>().useGravity = true;
        //InstParticle();
    }

    // Update is called once per frame
    void Update()
    {
        OriginPos.y = transform.position.y;

        if (Vector3.Distance(transform.position, OriginPos) > 4.0f)
        {
            
            transform.parent = null;
            Debug.Log("null Node");
        }
        if (Vector3.Distance(transform.position, OriginPos) > 0.2f)
        {
            if (transform.parent == null)
            {
                transform.LookAt(OriginPos);
                transform.Translate(new Vector3(0, 0, Time.deltaTime));
                //transform.Translate((transform.position - OriginPos).normalized * Time.deltaTime);
            }
        }
        base.Update();
    }

    public override bool Caption()
    {
        Debug.Log(gameObject.name + ",<color=red>Caption!!!</color>");
        if (!isCaption)
        {
            Instantiate(actionParticle, transform.position, Quaternion.identity).Play();
            isCaption = true;
            StopParticle();
            //Destroy(this);
            if (actionEffect != null)
            {
                gameObject.AddComponent<AudioSource>().clip = actionEffect;
                GetComponent<AudioSource>().Play();
            }
        }
        return false;
    }
}
