using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogBulletMove : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private Vector3 startPosition;//子弹初始位置

    void Awake()
    {
        rb=GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //限制子弹最远距离
        Vector3 currentPosition= transform.position;
        if ((currentPosition-startPosition).magnitude > 10)
            Destroy(gameObject);
    }

    //施加力的函数：力的方向和大小
    public void Launch(Vector2 direction,float force)
    {
        
        rb.AddForce(direction * force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        RobotControl robotControl = collision.gameObject.GetComponent<RobotControl>();
        if (robotControl != null)
        {
            robotControl.Fix();
        }
        Destroy(gameObject);
    }
    
}
