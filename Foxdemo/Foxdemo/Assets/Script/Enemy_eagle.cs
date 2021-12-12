using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_eagle : Enemy
{
    //������ӥ��״̬��up
    private bool isup = true;
    //���
    //private Animator anim;
    public Transform toppoint, bottompoint;
    //����
    private float topy;
    private float bottomy;
    //����
    public float flyforce;

    
    protected override void Start()
    {
        base.Start();
        //�����ײ��
        //��ø������
        rb = GetComponent<Rigidbody2D>();
        //��ֵ��
        topy = toppoint.position.y;
        bottomy = bottompoint.position.y;
        //�������µ�
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
    //�ƶ�
    void Movement()
    {
        //�����ӥ����
        if (isup)
        {
            //�����ٶ�
            rb.velocity = new Vector2(rb.velocity.x, flyforce);
            //��������һ����ʱ��,�ٶ��½�
            if (rb.position.y > topy)
            {
                rb.velocity = new Vector2(rb.velocity.x, -flyforce);
                isup = false;
            }
        }
        //�����ӥ�½�
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
