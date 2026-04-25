using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CiTIeCollision : MonoBehaviour
{
    public float CTSecond;
    private GameObject player;
    private GoodThings all;
    private SpriteRenderer spriteRenderer;//增加渲染

    private int thingsSpeed=5;//物品被磁铁吸过来的速度
    
    // Start is called before the first frame update
    void Start()
    {
        CTSecond = 6f;
        player = GameObject.FindWithTag("do");
        all=player.GetComponent<GoodThings>();
        gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer=GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.green;

        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;//保持位置同步
    }

    
    //物品被吸引的效果
    IEnumerator ThingsMove(Collider2D collision)
    {
        Vector3 dis=player.transform.position- collision.transform.position;
        Vector3 v=dis.normalized;
        float t = dis.magnitude / thingsSpeed;
        //保证物品飞到小鸟身上的时间在存在时间内，不会中途卡在路上
        if(t < CTSecond)
        {
            while (collision && dis.magnitude > 2)
            {
                Vector3 pos = v * thingsSpeed * Time.deltaTime;
                collision.transform.position += pos;
                yield return null;
            }
            all.Money(collision);//磁铁只响应金币和钻石的碰撞
        }        
    }

    //磁场存在时间计时器
    IEnumerator Timer()
    {
        while (CTSecond > 0)
        {
            CTSecond -= Time.deltaTime;
            yield return 0;
        }

        Destroy(gameObject);
    }

    //磁铁：吸引3米内的金币、钻石，持续6秒。
    private void OnTriggerStay2D(Collider2D collision)
    {        
        BoxCollider2D c = gameObject.GetComponent<BoxCollider2D>();//得到这个碰撞盒
        c.size = 4 * player.GetComponent<BoxCollider2D>().size;//设置碰撞盒大小
        c.isTrigger = true;//把它变为触发器
        //只响应钱类型的道具
        if (collision.gameObject.tag == "Award" || collision.gameObject.tag == "Award1" ||
            collision.gameObject.tag == "bigCorn" || collision.gameObject.tag == "ManyAward")
            StartCoroutine(ThingsMove(collision));
    }
}
