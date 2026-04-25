using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    public AudioClip HurtClip;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //使用协程：受伤动画不卡顿
    IEnumerator enumerator()
    {
        //animator.SetTrigger("IsHurt");//被雪花打到播放受伤动画
        audioSource.PlayOneShot(HurtClip);
        animator.Play("Hurt");
        yield return new WaitForSeconds(0.3f);
        //animator.ResetTrigger("IsHurt");
        animator.Play("Animation");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
        StartCoroutine(enumerator());
    }
}
