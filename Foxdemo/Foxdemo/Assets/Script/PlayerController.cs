using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    //下蹲关闭的碰撞箱
    public Collider2D DisColl;
    //判断是否顶头
    public Transform CeilingCheck,GroundCheck;
    //额外跳跃次数
    public int extrajump;
    //
    private bool isGround;
    



    void Start()
    {
    
    }

    
    void Update()
    {
        if (isHurt != true) 
        {
            Movement();
            Jump();
        }
        SwitchAnim();
        isGround = Physics2D.OverlapCircle(GroundCheck.position, 0.2f,ground);
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
            rb.velocity = new Vector2(Horizontalmove * speed, rb.velocity.y);
            //角色移动时的动作
            anim.SetFloat("running", Mathf.Abs(facedirection));
        }
        if (facedirection !=0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);

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
        Crouch();
    }
    //碰撞触发器
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //物品收集
        Collection collection = collision.gameObject.GetComponent<Collection>();
        if (collision.tag == "Collection")
        {
            collection.IsCollected();
            Cherry += 1;
            CherryNum.text = Cherry.ToString();
        }
        //死亡重开
        if(collision.tag == "DeadLine")
        {
            GetComponent<AudioSource>().enabled = false;
            Invoke("Restart", 2f);
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
    //下蹲
    void Crouch()
    {
        //检查头顶
        if (!Physics2D.OverlapCircle(CeilingCheck.position, 0.2f,ground)) 
        { 
            if (Input.GetButton("Crouch"))
            {
                anim.SetBool("crouching", true);
                DisColl.enabled = false;
            }
            else 
            {
                anim.SetBool("crouching", false);
                DisColl.enabled = true;
            }
        }
    }
    void Restart()
    {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void Jump()
    {
        if (isGround)
        {
            extrajump = 1;
        }
        if(Input.GetButtonDown("Jump")&& extrajump > 0)
        {
            rb.velocity = Vector2.up * jumpforce;
            extrajump--;
            anim.SetBool("jumping", true);
            jumpAudio.Play();
        }
        
    }
} 

