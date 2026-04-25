using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //控制器
    private CharacterController playerController;
    private Animator animator;

    private float moveSpeed = 3f;
    [Header("速度设置")]
    public float walkSpeed = 3f;
    public float runSpeed = 6f;

    private bool running;

    PlayerInteractor playerInteraction;
    // Start is called before the first frame update
    void Start()
    {
        //初始化
        playerController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        running = false;
        playerInteraction =  GetComponentInChildren<PlayerInteractor>();
    }

    // Update is called once per frame
    void Update()
    {
        //移动响应
        Move();
        //交互响应
        Interact();
    }

    //玩家交互
    public void Interact()
    {
        //鼠标左键（土地）
        if(Input.GetButtonDown("Fire1"))
        {
            playerInteraction.Interact();
        }
        //捡起物品
        if(Input.GetButtonDown("Fire2"))
        {
            playerInteraction.ItemInteract();
        }
    }

    //移动
    public void Move()
    {
        //得到移动值
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //计算方向和速度
        Vector3 dir=new Vector3(horizontal,0,vertical).normalized;
        Vector3 velocity = moveSpeed * dir * Time.deltaTime;

        //检查移动
        if(dir.magnitude >=0.1f) 
        {
            //转向
            transform.rotation = Quaternion.LookRotation(dir);

            //移动
            playerController.Move(velocity);
        }
        else
        {
            running = false;
            moveSpeed = running ? runSpeed : walkSpeed;
        }
        if(Input.GetKeyDown(KeyCode.LeftShift)||Input.GetKeyDown(KeyCode.RightShift))
        {
            running=!running;
            moveSpeed=running ? runSpeed : walkSpeed;
        }

        //改变动画
        animator.SetFloat("Speed",velocity.magnitude);
        animator.SetBool("Running", running);
    }
}
