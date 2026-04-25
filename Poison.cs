using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : GoodThings
{
    private bool isPoison = false;//是否吃毒
    private float PoisonSecond;//毒药持续时间
    private float totalSecond;//累计每一帧的时间
    private PlayerControl player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerControl>();
        totalSecond = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        poison();

    }

    
    //毒药专用
    private void poison()
    {
        if (isPoison)
        {
            //Debug.Log(PoisonSecond);           
            isPoison = Timer(PoisonSecond);
            PoisonSecond -= Time.deltaTime;//每一帧时间减少
            totalSecond += Time.deltaTime;//记录减少的时间
            if(totalSecond>=1-Time.deltaTime)//每秒扣除5生命值、增加2怒气值。减一个帧时间是防止误差导致没生效。
            {
                GameManager.Instance.printAll();
                GameManager.Instance.changeHP(-5);
                GameManager.Instance.changeAngry(2);
                GameManager.Instance.printAll();
                totalSecond = 0f;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //毒药：每秒扣除5生命值、增加2怒气值，持续6秒。
        if (collision.gameObject.tag == "Poison")
        {
            PoisonSecond = 6;
            isPoison = true;
            audioSource.PlayOneShot(fruitClip);
            Destroy(collision.gameObject);
        }
    }
}
