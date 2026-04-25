using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    //声明刚体变量，作为ruby自身的刚体
    private Rigidbody2D rigidbody2d;

    public int maxHealth = 5;//最大血量
    private int currentHealth;//当前血量
    public int Health {  get { return currentHealth; } }//用于获取当前生命值的属性
    
    private float speed=4f;//ruby速度

    public float timeInvincible = 2.0f;//无敌时间
    private bool isInvincible;//是否无敌状态
    private float invincibleTimer;//计时器

    private Vector2 lookDirection =new Vector2 (1,0);//朝向
    private Animator animator;//动画控制器组件

    public GameObject CogBulletPrefab;//子弹预制体

    private NPCDialog npcDialog;//对话框对象

    public AudioSource audioSource;//短音频
    public AudioSource walkAudioSource;//走路音频
    public AudioClip rubyHit;//受伤音效
    public AudioClip attackSoundClip;//发射音效
    public AudioClip walkSound;//走路音效

    private Vector3 replayPosition;//复活点
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        //获取ruby身上的刚体
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;

        isInvincible = false;//初始化无敌状态
        invincibleTimer = timeInvincible;

        animator = GetComponent<Animator>();//获得动画组件

        audioSource = GetComponent<AudioSource>();//获得声音组件
    
        replayPosition=transform.position;//记录存档点
    }

    // Update is called once per frame
    void Update()
    {
        //玩家输入监听
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2 (horizontal, vertical);
        //当某个轴向不为0，就可以转变动画
        if(!Mathf.Approximately(move.x,0)||!Mathf.Approximately(move.y,0))
        {//上面是：判断是否近似相等函数
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
            if(!walkAudioSource.isPlaying)//播放走路音效
            {
                walkAudioSource.clip = walkSound;
                walkAudioSource.Play();
            }
        }
        else
        {
            //停止时不播放
            if(walkAudioSource.isPlaying)
                walkAudioSource.Stop();
            
        }
        //数据连接到动画
        animator.SetFloat("Look X", lookDirection.x);//控制方向
        animator.SetFloat("Look Y",lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);//控制走动和静止

        //计算ruby的移动
        Vector2 position=transform.position;
        ////水平方向移动
        //position.x = position.x + speed* horizontal*Time.deltaTime;//每秒15米
        ////垂直方向移动
        //position.y = position.y + speed * vertical * Time.deltaTime;
        position += speed * move * Time.deltaTime;
        
        //transform.position = position;
        //把上面计算出来的位置给刚体（这种方法用于防止刚体和位移同时作用，造成碰撞有抖动）
        rigidbody2d.MovePosition(position);
        
        //无敌时间
        if(isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if(invincibleTimer <= 0)
            {
                isInvincible = false;
            }
        }

        //监听玩家输入发射键
        if(Input.GetMouseButtonDown(0))
        {
            Launch();
        }

        //检测npc对话
        if(Input.GetKeyDown(KeyCode.F))
        {
            //射线检测，起点：ruby中心，方向：ruby朝向，距离1.5，层级NPC
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position +
                Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if(hit.collider != null)
            {
                npcDialog=hit.collider.GetComponent<NPCDialog>();
                if (npcDialog!=null)
                {
                    npcDialog.DisplayDialog(true);
                }
            }
        }
        //关闭对话框
        if(Input.GetMouseButtonDown(1)&& npcDialog)
        {
            npcDialog.DisplayDialog(false);
        }
    }

    //生命值变化函数，生命值最小0，最大为设定值
    public void changeHealth(int value)
    {
        if (value<0)
        {
            if (isInvincible)
                return;
            isInvincible = true;
            invincibleTimer = timeInvincible;
            PlaySound(rubyHit);//受伤音效
            animator.SetTrigger("Hit");//受伤动作切换
        }
        currentHealth=Mathf.Clamp(currentHealth+value, 0, maxHealth);
        //Debug.Log(currentHealth + "/" + maxHealth);
        //同步UI值
        RubyHealthBar.instance.SetValue(currentHealth/(float)maxHealth);
    
        if(currentHealth<=0)//死亡复活
        {
            Respawn();
        }
    }

    //玩家发射子弹
    private void Launch()
    {
        if (!RubyHealthBar.instance.hasTask)
            return;
        //创建子弹
        GameObject projectCog = Instantiate(CogBulletPrefab, 
            rigidbody2d.position+Vector2.up*0.5f, Quaternion.identity);
        //调用子弹方法
        CogBulletMove cm=projectCog.GetComponent<CogBulletMove>();
        cm.Launch(lookDirection, 300);
        //ruby动画切换
        animator.SetTrigger("Launch");
        //音效
        PlaySound(attackSoundClip);
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    private void Respawn()//复活
    {
        changeHealth(maxHealth);
        transform.position = replayPosition;
    }
}
