using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager Instance { get; private set; }//全局唯一实例

    private int score;//积分
    private int count;//连击数

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);//如有实例化则销毁对象

        }
        else
        {
            Instance = this;//赋值实例
        }
    }

    void Start()
    {
        score = 0;
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //积分变化函数
    public void changeScore(int value)
    {
        //得分机制：
        //连击30次以下100分，
        //连击30~50次200分，
        //当连击50~70次300分，
        //当连击70次以上500分。
        Debug.Log("积分增加");
        //积分无上限，但避免负数
        if (count < 30)
        {
            score = Mathf.Max(score + value*1, 0);
        }
        else if (count >= 30&&count<50) {
            score = Mathf.Max(score + value * 2, 0);
        }
        else if(count >= 50&&count<70)
        {
            score = Mathf.Max(score + value * 3, 0);
        }
        else
        {
            score = Mathf.Max(score + value * 5, 0);
        }
    }

    //连击数变化函数
    public void changeCount(int value)
    {
        
        if (value == 0)
        {//连击数清零
            count = 0;
        }
        else
        {
            Debug.Log("点击");
            //连击数无上限，但避免负数
            count = Mathf.Max(count + value, 0);
        }
        
    }

    //输出信息函数:积分、连击数
    public void printAll()
    {
        Debug.Log("积分：" + score
            +"连击数："+count);
    }

    public int GetScore()
    {
        return score;
    }
    public int GetCombo()
    {
        return count;
    }
}
