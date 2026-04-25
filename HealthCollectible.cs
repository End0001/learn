 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    RubyController rb;
    public AudioClip audioClip;//音效
    public GameObject pickUpEffect;//特效
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {//吃草莓回血1
        rb=collision.GetComponent<RubyController>();//得到ruby
        if(rb!=null)//判空（因为存在不是ruby碰到草莓的情况）
        {
            rb.changeHealth(1);
            rb.PlaySound(audioClip);//播放吃草莓音效
            Instantiate(pickUpEffect, transform.position, Quaternion.identity);//吃草莓特效
            Destroy(gameObject);
        }
    }
}
