using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F : DFJK
{
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            sr.color = Color.white;
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            sr.color = new Color(0.68f, 0.68f, 0.68f, 1f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.F)|| Input.GetKeyUp(KeyCode.F))
        {
            Destroy(collision.gameObject);
            SnowEffect.Play();//击中雪花特效
            audioSource.PlayOneShot(snowClip);//击中雪花音效
            GameManager.Instance.changeCount(1);//点到连击数加一
            GameManager.Instance.changeScore(100);//点到音符加100分
            GameManager.Instance.printAll();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyUp(KeyCode.F))
        {
            Destroy(collision.gameObject);
            SnowEffect.Play();//击中雪花特效
            audioSource.PlayOneShot(snowClip);//击中雪花音效
            GameManager.Instance.changeCount(1);//点到连击数加一
            GameManager.Instance.changeScore(100);//点到音符加100分
            GameManager.Instance.printAll();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!Input.GetKeyDown(KeyCode.F) || Input.GetKeyUp(KeyCode.F))
        {
            GameManager.Instance.changeCount(0);//没点到
            GameManager.Instance.printAll();
        }
    }
}
