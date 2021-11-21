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
    public float speed;
    //Ԥ���ȡ��������
    private float leftx, rightx;



    void Start()
    {
        //��ȡ���� 
        rb = GetComponent<Rigidbody2D>();
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
        Movement();
    }
    void Movement()
    {
        //����泯���
        if (faceleft)
        {
            //forg���ٶ�����Ϊ������
            rb.velocity = new Vector2(-speed, rb.transform.position.y);
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
            rb.velocity = new Vector2(speed, rb.transform.position.y);
            if (rb.transform.position.x > rightx)
            {
                rb.transform.localScale = new Vector3(1, 1, 1);
                faceleft = true;
            }
        }
    }
}



