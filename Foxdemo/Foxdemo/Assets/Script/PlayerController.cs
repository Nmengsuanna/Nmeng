using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //控制组件刚体
    public Rigidbody2D rb;
    //变量速度
    private float speed;
    //变量跳跃力度
    public float jumpforce;
    //控制组件动画器
    public Animator anim;
    //设置地面
    public LayerMask ground;
    //碰撞体
    private CapsuleCollider2D capColl;
    private Collider2D edgColl;
    //樱桃数目
    public int Cherry = 0;
    public int Gem = 0;
    //樱桃UI数目
    //public Text CherryNum;
    //是否受伤
    private bool isHurt;
    //传入音源
    public AudioSource jumpAudio;
    //判断是否顶头
    public Transform CeilingCheck,GroundCheck;
    //额外跳跃次数
    public int extrajump;
    //判断是否在地面
    private bool isGround;
    //
    private bool death = false;
    //
    Vector2 colliderStandSize;
    Vector2 colliderStandOffset;
    Vector2 colliderCrouchSize;
    Vector2 colliderCrouchOffset;
    //判断是否是下落
    private bool falling = false;
    //获得角色方位
    private Transform playerTransform;
    //樱桃数量UI
    public TMP_Text CherryNum;
    //钻石数量UI
    public TMP_Text GemNum;



    void Start()
    {
        edgColl = GetComponent<EdgeCollider2D>();
        capColl = GetComponent<CapsuleCollider2D>();
        colliderStandSize = capColl.size;
        colliderStandOffset = capColl.offset;
        colliderCrouchSize = new Vector2(capColl.size.x, capColl.size.y * 0.5f);
        colliderCrouchOffset = new Vector2(capColl.offset.x, capColl.offset.y * 1.5f);
        playerTransform = GetComponent<Transform>();
        speed = 8f;
    }

    
    void Update()
    {
        if (isHurt != true && death == false) 
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
                falling = true;
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        else if (rb.velocity.y < 0)
        {
            falling = true;
        }
        else if (isHurt)
        {
            if (Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                isHurt = false;
                anim.SetBool("hurt", false);
            }
        }
        else if (capColl.IsTouchingLayers(ground))
        {
            falling = false;
            anim.SetBool("falling", false);
        }
        Crouch();
    }
    //碰撞触发器
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //物品收集
        Collection collection = collision.gameObject.GetComponent<Collection>();
        if (collision.tag == "Cherry")
        {
            collection.gameObject.tag = "Dead";
            collection.IsCollected();
            Cherry += 1;
            CherryNum.text = Cherry.ToString();
        }
        if (collision.tag == "Gem")
        {
            collection.gameObject.tag = "Dead";
            collection.IsCollected();
            Gem += 1;
            GemNum.text = Gem.ToString();
        }
        //死亡重开
        if (collision.tag == "DeadLine")
        {
            death = true;
            rb.velocity = new Vector2(0, 0);
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
            if (falling == true && playerTransform.position.y > enemy.transform.position.y)
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
        if (!Physics2D.OverlapCircle(CeilingCheck.position, 0.1f,ground)) 
        { 
            if (Input.GetButton("Crouch"))
            {
                speed = 4f;
                capColl.size = colliderCrouchSize;
                capColl.offset = colliderCrouchOffset;
                //edgColl.enabled = false;
                anim.SetBool("crouching", true);
                //DisColl.enabled = false;
            }
            else 
            {
                speed = 8f;
                capColl.size = colliderStandSize;
                capColl.offset = colliderStandOffset;
                //edgColl.enabled = true;
                anim.SetBool("crouching", false);
                //DisColl.enabled = true;
            }
        }
    }
    //重开游戏
    void Restart()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    //可多段跳跳跃
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
            anim.SetBool("falling", false);
            jumpAudio.Play();
        }
        
    }
} 

