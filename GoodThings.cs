using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodThings : MonoBehaviour
{
    private PlayerControl p;
    private CiTIeCollision ct;
    private timebar timeBar;

    public AudioSource audioSource;//小鸟身上的播放器
    public AudioClip coinClip;//吃金币
    public AudioClip CTClip;//磁铁
    public AudioClip awardClip;//钻石
    public AudioClip fruitClip;//果子
    public AudioClip clockClip;//时钟
    public AudioClip bigCoinClip;//金条

    private int thingsSpeed = 3;//物品被磁铁吸过来的速度
    // Start is called before the first frame update
    void Start()
    {
        p= GetComponent<PlayerControl>();
        if(GameObject.FindWithTag("CTCollision"))//找场景中有没有磁场碰撞盒
            ct=FindObjectOfType<CiTIeCollision>();
        audioSource=GetComponent<AudioSource>();
        timeBar=FindAnyObjectByType<timebar>();
    }

    // Update is called once per frame
    void Update()
    {

    }



    //计时器
    public bool Timer(float t)
    {
        if (t < 0)
        {
            return false;
        }
        //Debug.Log(t);
        return true;
    }


    public void Money(Collider2D collision)//封装一下金币和钻石
    {
        
        //金币:增加1金币。增加5积分，增加1怒气值。
        if (collision.gameObject.tag == "Award")
        {
            //StartCoroutine(ThingsMove(collision));
            GameManager.Instance.printAll();
            GameManager.Instance.changeCoins(1);
            GameManager.Instance.changeScore(5);
            GameManager.Instance.changeAngry(1);
            GameManager.Instance.printAll();
            audioSource.PlayOneShot(coinClip);//吃金币音效
            Destroy(collision.gameObject);
        }
        //钻石:增加5金币。增加25积分，增加5怒气值。
        if (collision.gameObject.tag == "Award1")
        {
            //StartCoroutine(ThingsMove(collision));
            GameManager.Instance.printAll();
            GameManager.Instance.changeCoins(5);
            GameManager.Instance.changeScore(25);
            GameManager.Instance.changeAngry(5);
            GameManager.Instance.printAll();
            audioSource.PlayOneShot(awardClip);//钻石音效
            Destroy(collision.gameObject);
        }
        //金条：增加10金币、30积分、10贼胆值。
        if (collision.gameObject.tag == "bigCorn")
        {
            //StartCoroutine(ThingsMove(collision));
            GameManager.Instance.printAll();
            GameManager.Instance.changeCoins(10);
            GameManager.Instance.changeScore(30);
            GameManager.Instance.changeAngry(10);
            GameManager.Instance.printAll();
            audioSource.PlayOneShot(bigCoinClip);
            Destroy(collision.gameObject);
        }
        //珠宝珍珠：增加20金币、40积分、20贼胆值。
        if (collision.gameObject.tag == "ManyAward")
        {
            //StartCoroutine(ThingsMove(collision));
            GameManager.Instance.printAll();
            GameManager.Instance.changeCoins(20);
            GameManager.Instance.changeScore(40);
            GameManager.Instance.changeAngry(20);
            GameManager.Instance.printAll();
            audioSource.PlayOneShot(coinClip);//钻石音效
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Money(collision);
        //磁铁：增加磁铁碰撞盒
        if (collision.gameObject.tag == "CiTie")
        {
            audioSource.PlayOneShot(CTClip);
            if (GameObject.Find("CTCollision")==null)
            {
                GameObject CTCollision = new GameObject("CTCollision");//添加一个放磁场的空对象
                //CTCollision.transform.position = transform.position;//设置位置
                CTCollision.tag = "CTCollision";//添加标签
                //CTCollision.transform.parent = transform;//设置成小鸟的子对象
                CTCollision.AddComponent<CiTIeCollision>();//给磁场对象增加脚本
                if (CTCollision.GetComponent<BoxCollider2D>() == null)//给磁场对象增加碰撞盒
                    CTCollision.AddComponent<BoxCollider2D>();
                Destroy(collision.gameObject);
            }
            else
            {
                if (GameObject.FindWithTag("CTCollision"))//找场景中有没有磁场碰撞盒
                    ct = FindObjectOfType<CiTIeCollision>();
                ct.CTSecond = 6;//如果身上已有磁铁，又吃到磁铁 则刷新持续时间
                Destroy(collision.gameObject);
            }
        }
        //果子：
        //1.回复30生命值。
        //2.生命值满时，变为增加50金币
        if (collision.gameObject.tag=="fruit")
        {
            audioSource.PlayOneShot(fruitClip);//音效
            GameManager.Instance.printAll();
            if (GameManager.Instance.currentHP==100)            
                GameManager.Instance.changeCoins(50);
            else
                GameManager.Instance.changeHP(30);
            GameManager.Instance.printAll();
            Destroy(collision.gameObject);
        }
        //时间道具
        if (collision.tag == "goodClock")
        {
            audioSource.PlayOneShot(clockClip);//音效
            //时间再次高于10s时，时间即将耗尽还可以再播放提示音效（勿改）
            if (GameManager.Instance.currentT+5>=10)
            {
                timeBar.CanClip=true;
            }
            GameManager.Instance.printAll();
            GameManager.Instance.changeTime(5);//正面道具加5s
            GameManager.Instance.printAll();
            Destroy(collision.gameObject);
        }
        if (collision.tag == "badClock")
        {
            audioSource.PlayOneShot(clockClip);//音效
            GameManager.Instance.printAll();
            GameManager.Instance.changeTime(-10);//负面道具减10s
            GameManager.Instance.printAll();
            Destroy(collision.gameObject);
        }
        //停止牌：减少10金币、10贼胆值。
        if (collision.tag == "Stop")
        {
            GameManager.Instance.printAll();
            GameManager.Instance.changeCoins(-10);
            GameManager.Instance.changeAngry(-10);
            GameManager.Instance.printAll();
            Destroy(collision.gameObject);
        }

    }

    
}
