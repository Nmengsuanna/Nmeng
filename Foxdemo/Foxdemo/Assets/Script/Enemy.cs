using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anim;
    protected AudioSource deathAudio;

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
        deathAudio.Play();
        anim.SetTrigger("Death");
    }
}


    