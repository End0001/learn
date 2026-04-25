using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : GoodThings
{
    //private bool isBoom = false;//是否被炸
    private PlayerControl player;
   
    private Animator policeAni;
   
    public AudioClip hurtClip;//受伤音效

    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerControl>();
        policeAni = GameObject.Find("police").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }   

    //减速效果存在时间的计时器
    IEnumerator BoomTimer(float time)
    {
        while (GameManager.Instance.isInvincible==false&& time > 0)
        {

            GameManager.Instance.speed = 2f;
            time -= Time.deltaTime;
            yield return 0;
        }
        player.SwitchAnimate("fly",false);//飞行，非快速切换，为播放完成后切换
        policeAni.SetBool("In", false);
        GameManager.Instance.speed = 4f;
        GameManager.Instance.isBoom = false;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //炸弹:扣除25生命值，增加15怒气值，眩晕1.5秒。
        if (collision.gameObject.tag == "Barrier")
        {
            StartCoroutine(BoomTimer(1.5f));
            if(!GameManager.Instance.isInvincible) player.SwitchAnimate("yun", true);
            policeAni.SetBool("In", true);
            GameManager.Instance.printAll();
            GameManager.Instance.changeHP(-25);
            GameManager.Instance.changeAngry(15);//非愤怒状态下改变
            GameManager.Instance.printAll();

            GameManager.Instance.isBoom = true;
            audioSource.PlayOneShot(hurtClip);
            Destroy(collision.gameObject);
        }
    }
}
