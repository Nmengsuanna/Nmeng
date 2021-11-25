using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anim;
    protected AudioSource deathAudio;

    //将start设置成虚拟的，也就是可以被子类改写的。
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        deathAudio = GetComponent<AudioSource>();
    }
    //死亡消灭实体
    public void Death()
    {
        Destroy(gameObject);
    }
    //触发死亡特效
    public void JumpOn()
    {
        deathAudio.Play();
        anim.SetTrigger("Death");
    }
}


    