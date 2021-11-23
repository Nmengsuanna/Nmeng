using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_frog : MonoBehaviour
{
    //��ȡ����
    private Rigidbody2D rb;
    //�������ҵ�����
    public Transform leftpoint, rightpoint;
    //�����泯��
    private bool faceleft = true;
    //����frog�ٶ�
    public float speed, jumpforce;
    //Ԥ���ȡ��������
    private float leftx, rightx;
    //�������������
    private Animator anim;
    //
    public LayerMask Ground;
    //����2D��ײ��
    private Collider2D coll;



    void Start()
    {
        //��ȡ���� 
        rb = GetComponent<Rigidbody2D>();
        //��ö��������
        anim = GetComponent<Animator>();
        //�����ײ�����
        coll = GetComponent<Collider2D>();
        //������������
        leftx = leftpoint.transform.position.x;
        rightx = rightpoint.transform.position.x;
        //�������ҵ�
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
        //����泯���
        if (faceleft)
        {
            //forg�����ذ�ʱ,�ٶ�����Ϊ������,������Ծ
            if (coll.IsTouchingLayers (Ground))
            {
                anim.SetBool("jump", true);
                rb.velocity = new Vector2(-speed, jumpforce);
            }
            
            //���˵����λ�ó��������
            if (rb.transform.position.x < leftx)
            {
                //ת�� 
                rb.transform.localScale = new Vector3(-1, 1, 1);
                //����
                faceleft = false;
            }
        }
        else
        {
            if (coll.IsTouchingLayers(Ground))
            {
                anim.SetBool("jump", true);
                rb.velocity = new Vector2(speed, jumpforce);
            }
            if (rb.transform.position.x > rightx)
            {
                rb.transform.localScale = new Vector3(1, 1, 1);
                faceleft = true;
            }
        }
    }
    //�ı䶯��
    void SwitchAnim()
    {
        //��������Ծ״̬��ʱ�����˵�ٶ�Ϊ0����������Ծ״̬ȡ��������״̬����
        if (anim.GetBool("jump"))
        {
            if (rb.velocity.y < 0.1)
            {
                anim.SetBool("jump", false);
                anim.SetBool("fall", true);
                
            }
        }
        //������������ذ岢���Ǵ�������״̬�������Ľ���״̬ȡ��
        if(coll.IsTouchingLayers(Ground) && anim.GetBool("fall"))
        {
            anim.SetBool("fall", false);

        }
    }
}





