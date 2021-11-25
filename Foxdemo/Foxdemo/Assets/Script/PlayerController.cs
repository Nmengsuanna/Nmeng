using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //控制组件刚体
    public Rigidbody2D rb;
    //变量速度
    public float speed;
    //变量跳跃力度
    public float jumpforce;
    //控制组件动画器
    public Animator anim;
    //
    public LayerMask ground;
    //
    public Collider2D coll;
    //
    public int Cherry = 0;
    //樱桃UI数目
    public Text CherryNum;
    //是否受伤
    private bool isHurt;
    //传入音源
    public AudioSource jumpAudio;



    void Start()
    {
    
    }

    
    void Update()
    {
        if (isHurt != true) 
        {
            Movement();
        }
        SwitchAnim();
    }
    //角色移动
    void Movement()
    {
        float Horizontalmove;
        float facedirection;


        Horizontalmove = Input.GetAxis("Horizontal");
        facedirection = Input.GetAxisRaw("Horizontal");

        //角色移动
        if (Horizontalmove != 0) {
            rb.velocity = new Vector2(Horizontalmove * speed , rb.velocity.y);
            //角色移动时的动作
            anim.SetFloat("running", Mathf.Abs(facedirection));
        }
        if (facedirection !=0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);

        }
       //角色跳跃
        if(Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            jumpAudio.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            anim.SetBool("jumping", true);
         
        }
    }
    
    //改变动画
    void SwitchAnim()
    {
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("idle", false);
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        else if (isHurt)
        {
            if (Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                isHurt = false;
                anim.SetBool("hurt", false);
                anim.SetBool("idle", true); 
            }
        }
        else if (coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", false);
            anim.SetBool("idle", true);
        }
    }
    //收集物品
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collection")
        {
            Destroy(collision.gameObject);
            Cherry += 1;
            CherryNum.text = Cherry.ToString();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //有关敌人
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy  = collision.gameObject.GetComponent<Enemy>();
            //消灭敌人
            if (anim.GetBool("falling"))
            {
                enemy.JumpOn();
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                anim.SetBool("jumping", true);
            }
            //碰到敌人受伤后反弹
            else if (transform.position.x < collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-10f, rb.velocity.y);
                isHurt = true;
                anim.SetBool("hurt", true);
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(10f, rb.velocity.y);
                isHurt = true;
                anim.SetBool("hurt", true);
            }
        }     
    }
} 

