using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anim;

    //��start���ó�����ģ�Ҳ���ǿ��Ա������д�ġ�
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
    }
    //��������ʵ��
    public void Death()
    {
        Destroy(gameObject);
    }
    //����������Ч
    public void JumpOn()
    {
        anim.SetTrigger("Death");
    }
}


    