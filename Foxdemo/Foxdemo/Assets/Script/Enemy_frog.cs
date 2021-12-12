using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_frog : Enemy
{
    //导入左右点坐标
    public Transform leftpoint, rightpoint;
    //设置面朝向
    private bool faceleft = true;
    //设置frog速度
    public float speed, jumpforce;
    //预设获取参数名字
    private float leftx, rightx;
    
    //创建动画器组件
    //private Animator anim;
    
    //导入地面
    public LayerMask Ground;
    
    


    //将start设置成重载的，也就是可以将内容重新写入父类中以取代原本父类的内容
    protected override void Start()
    {
        //引用父级的start的内容,内容包括了获得动画组件
        base.Start();
        //获取刚体 
        rb = GetComponent<Rigidbody2D>();
        
        //获得动画器组件
        //anim = GetComponent<Animator>();
        
        //获得碰撞体组件
        coll = GetComponent<Collider2D>();
        //获得左右坐标点
        leftx = leftpoint.transform.position.x;
        rightx = rightpoint.transform.position.x;
        //消灭左右点
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        SwitchAnim();
    }
    void Movement()
    {
        //如果面朝左边
        if (faceleft)
        {
            //如果说他的位置超过了左点
            if (rb.transform.position.x < leftx)
            {
                //转向 
                rb.transform.localScale = new Vector3(-1, 1, 1);
                //朝右
                faceleft = false;
            }
            
            //forg触碰地板时,速度设置为负方向,向上跳跃
            else if (coll.IsTouchingLayers (Ground))
            {
                anim.SetBool("jump", true);
                rb.velocity = new Vector2(-speed, jumpforce);
            }
            
        }
        else
        {
            if (rb.transform.position.x > rightx)
            {
                rb.transform.localScale = new Vector3(1, 1, 1);
                faceleft = true;
            }
            else if (coll.IsTouchingLayers(Ground))
            {
                anim.SetBool("jump", true);
                rb.velocity = new Vector2(speed, jumpforce);
            }
            
        }
    }
    //改变动画
    void SwitchAnim()
    {
        //敌人在跳跃状态的时候如果说速度为0，将他的跳跃状态取消，掉落状态开启
        if (anim.GetBool("jump"))
        {
            if (rb.velocity.y < 0.1)
            {
                anim.SetBool("jump", false);
                anim.SetBool("fall", true);
                
            }
        }
        //如果敌人碰到地板并且是处于下落状态，将他的降落状态取消
        if(coll.IsTouchingLayers(Ground) && anim.GetBool("fall"))
        {
            anim.SetBool("fall", false);

        }
    }
}





