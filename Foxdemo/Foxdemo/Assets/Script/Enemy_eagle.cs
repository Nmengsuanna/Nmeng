using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_eagle : Enemy
{
    //设置老鹰的状态是up
    private bool isup = true;
    //组件
    //private Animator anim;
    public Transform toppoint, bottompoint;
    //存量
    private float topy;
    private float bottomy;
    //参数
    public float flyforce;

    
    protected override void Start()
    {
        base.Start();
        //获得碰撞体
        //获得刚体组件
        rb = GetComponent<Rigidbody2D>();
        //赋值给
        topy = toppoint.position.y;
        bottomy = bottompoint.position.y;
        //消灭上下点
        Destroy(toppoint.gameObject);
        Destroy(bottompoint.gameObject);
        
        
    }

    
    void Update()
    {
        if (jumpOn == false)
        {
            Movement();
        }
    }
    //移动
    void Movement()
    {
        //如果老鹰上升
        if (isup)
        {
            //给予速度
            rb.velocity = new Vector2(rb.velocity.x, flyforce);
            //当上升到一定的时候,速度下降
            if (rb.position.y > topy)
            {
                rb.velocity = new Vector2(rb.velocity.x, -flyforce);
                isup = false;
            }
        }
        //如果老鹰下降
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, -flyforce);
            if (rb.position.y < bottomy)
            {
                rb.velocity = new Vector2(rb.velocity.x, flyforce);
                isup = true;
            }
        }
    }
    
}
