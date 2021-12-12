using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//该类中已经有Animator，AudioSource，Rigidbody2D，无须再次引用
public class Enemy : MonoBehaviour
{
    protected Animator anim;
    protected AudioSource deathAudio;
    protected Rigidbody2D rb;
    protected Collider2D coll;
    protected bool jumpOn;

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
        jumpOn = true;
        gameObject.tag = "Dead";
        Destroy(GetComponent<Rigidbody2D>());
        GetComponent<Collider2D>().enabled = false;
        deathAudio.Play();
        anim.SetTrigger("Death");
    }

}


    