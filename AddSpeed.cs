using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSpeed : GoodThings
{
    public AudioClip AddSpeedClip;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //加速靴的计时器
    IEnumerator ASTimer(float time)
    {
        while (time > 0)
        {
            GameManager.Instance.speed = 6f;
            time -= Time.deltaTime;
            yield return 0;
        }
        GameManager.Instance.speed = 4f;
    }

    //减速带的计时器
    IEnumerator RSTimer(float time)
    {
        while (time > 0)
        {
            GameManager.Instance.speed = 2f;
            time -= Time.deltaTime;
            yield return 0;
        }
        GameManager.Instance.speed = 4f;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //加速靴：速度变为5m/s，持续3s（必须比技能速度、时间小）
        if (collision.gameObject.tag == "AddSpeed")
        {
            StartCoroutine(ASTimer(3f));
            audioSource.PlayOneShot(AddSpeedClip);
            Destroy(collision.gameObject);
        }
        //减速带：速度变为3m/s，持续3s
        if (collision.gameObject.tag == "ReduceSpeed")
        {
            StartCoroutine(RSTimer(3f));
            Destroy(collision.gameObject);
        }
    }
}
