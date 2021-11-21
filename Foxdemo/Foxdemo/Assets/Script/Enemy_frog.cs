using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_frog : MonoBehaviour
{
    //获取刚体
    private Rigidbody2D rb;
    //导入左右点坐标
    public Transform leftpoint, rightpoint;

    //设置面朝向
    private bool faceleft = true;

    //设置frog速度
    public float speed;
    //预设获取参数名字
    private float leftx, rightx;



    void Start()
    {
        //获取刚体 
        rb = GetComponent<Rigidbody2D>();
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
        Movement();
    }
    void Movement()
    {
        //如果面朝左边
        if (faceleft)
        {
            //forg的速度设置为负方向
            rb.velocity = new Vector2(-speed, rb.transform.position.y);
            //如果说他的位置超过了左点
            if (rb.transform.position.x < leftx)
            {
                //转向 
                rb.transform.localScale = new Vector3(-1, 1, 1);
                //朝右
                faceleft = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(speed, rb.transform.position.y);
            if (rb.transform.position.x > rightx)
            {
                rb.transform.localScale = new Vector3(1, 1, 1);
                faceleft = true;
            }
        }
    }
}



