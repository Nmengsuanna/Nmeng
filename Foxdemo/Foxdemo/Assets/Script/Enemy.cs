using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�������Ѿ���Animator��AudioSource��Rigidbody2D�������ٴ�����
public class Enemy : MonoBehaviour
{
    protected Animator anim;
    protected AudioSource deathAudio;
    protected Rigidbody2D rb;
    protected Collider2D coll;
    protected bool jumpOn;

    //��start���ó�����ģ�Ҳ���ǿ��Ա������д�ġ�
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        deathAudio = GetComponent<AudioSource>();
        
    }
    //��������ʵ��
    public void Death()
    {
        Destroy(gameObject);
    }
    //����������Ч
    public void JumpOn()
    {
        jumpOn = true;
        gameObject.tag = "Dead";
        Destroy(GetComponent<Rigidbody2D>());
        GetComponent<Collider2D>().enabled = false;
        deathAudio.Play();
        anim.SetTrigger("Death");
    }

}


    