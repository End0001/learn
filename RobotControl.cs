using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotControl : MonoBehaviour
{
    private float speed=2;
    private Rigidbody2D rb;
    public bool vertical=false;//轴向
    private int direction=1;//移动方向
    private float changeTime = 3;//方向切换时间间隔
    private float timer;//计时器

    RubyController rbController;//碰撞怪的玩家
    
    private Animator animator;//动画状态机

    private bool broken=true;//是否故障

    public ParticleSystem particleS;//机器人身上的粒子烟雾

    private AudioSource audioSource;//音频播放器
    public AudioClip fixedSound;//修好音效
    public AudioClip []hitSound;//被打音效

    public GameObject hitEffect;//被打中特效
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        rb = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        //animator.SetFloat("MoveX", direction);
        //animator.SetBool("Vertical", vertical);
        ChangeAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        if (!broken) return;//修好了就不执行下面代码

        Vector2 position = rb.position;//刚体坐标

        timer -= Time.deltaTime;
        if (timer < 0)//每隔一段时间走反方向
        {
            direction = -direction;
            //animator.SetFloat("MoveX", direction);//同步改变动画
            ChangeAnimation();
            timer = changeTime;
        }

        if (vertical)//垂直轴向
        {
            position.y += Time.deltaTime * speed*direction;
        }
        else//水平轴向
            position.x += Time.deltaTime * speed * direction;
        rb.position = position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rbController = collision.gameObject.GetComponent<RubyController>();
        if (rbController != null)
        {
            rbController.changeHealth(-1);
        }
    }

    //动画切换
    private void ChangeAnimation()
    {
        if (vertical)//垂直轴向
        {
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", direction);
        }
        else//水平轴向
        {
            animator.SetFloat("MoveX", direction);
            animator.SetFloat("MoveY", 0);
        }
    }

    public void Fix()
    {
        broken = false;
        rb.simulated = false;//不进行碰撞，不会扣玩家的血
        animator.SetTrigger("Fixed");//动画切换
        particleS.Stop();//停掉烟雾特效
        //击打特效
        Instantiate(hitEffect, transform.position, Quaternion.identity);
        //随机播放被打音效
        int randomNum = Random.Range(0, 2);
        audioSource.Stop();
        audioSource.PlayOneShot(hitSound[randomNum]);       
        Invoke("PlayFixedSound", 1f);//延时调用
        //修理数量加一
        RubyHealthBar.instance.fixedNum++;
    }

    private void PlayFixedSound()
    {
        audioSource.PlayOneShot(fixedSound);//修好音效播放
    }
}
