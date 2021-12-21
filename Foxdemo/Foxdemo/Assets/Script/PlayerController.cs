using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //�����������
    public Rigidbody2D rb;
    //�����ٶ�
    public float speed;
    //������Ծ����
    public float jumpforce;
    //�������������
    public Animator anim;
    //���õ���
    public LayerMask ground;
    //��ײ��
    private CapsuleCollider2D capColl;
    private BoxCollider2D boxColl;
    //ӣ����Ŀ
    public int Cherry = 0;
    //ӣ��UI��Ŀ
    public Text CherryNum;
    //�Ƿ�����
    private bool isHurt;
    //������Դ
    public AudioSource jumpAudio;
    //�ж��Ƿ�ͷ
    public Transform CeilingCheck,GroundCheck;
    //������Ծ����
    public int extrajump;
    //�ж��Ƿ��ڵ���
    private bool isGround;
    //
    private bool death = false;
    //
    Vector2 colliderStandSize;
    Vector2 colliderStandOffset;
    Vector2 colliderCrouchSize;
    Vector2 colliderCrouchOffset;
    



    void Start()
    {
        boxColl = GetComponent<BoxCollider2D>();
        capColl = GetComponent<CapsuleCollider2D>();
        colliderStandSize = capColl.size;
        colliderStandOffset = capColl.offset;
        colliderCrouchSize = new Vector2(capColl.size.x, capColl.size.y * 0.5f);
        colliderCrouchOffset = new Vector2(capColl.offset.x, capColl.offset.y * 1.5f);
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
    //��ɫ�ƶ�
    void Movement()
    {
        float Horizontalmove;
        float facedirection;


        Horizontalmove = Input.GetAxis("Horizontal");
        facedirection = Input.GetAxisRaw("Horizontal");

        //��ɫ�ƶ�
        if (Horizontalmove != 0) {
            rb.velocity = new Vector2(Horizontalmove * speed, rb.velocity.y);
            //��ɫ�ƶ�ʱ�Ķ���
            anim.SetFloat("running", Mathf.Abs(facedirection));
        }
        if (facedirection !=0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);

        }
       
    }
    
    //�ı䶯��
    void SwitchAnim()
    {
        if (rb.velocity.y < 0)
            {
                anim.SetBool("idle", false);
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
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
        else if (capColl.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", false);
            anim.SetBool("idle", true);
        }
        Crouch();
    }
    //��ײ������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //��Ʒ�ռ�
        Collection collection = collision.gameObject.GetComponent<Collection>();
        if (collision.tag == "Collection")
        {
            collection.gameObject.tag = "Dead";
            collection.IsCollected();
            Cherry += 1;
            CherryNum.text = Cherry.ToString();
        }
        //�����ؿ�
        if(collision.tag == "DeadLine")
        {
            death = true;
            rb.velocity = new Vector2(0, 0);
            GetComponent<AudioSource>().enabled = false;
            Invoke("Restart", 2f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�йص���
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy  = collision.gameObject.GetComponent<Enemy>();
            //�������
            if (anim.GetBool("falling"))
            {
                enemy.JumpOn();
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                anim.SetBool("jumping", true);
            }
            //�����������˺󷴵�
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
    //�¶�
    void Crouch()
    {
        //���ͷ��
        if (!Physics2D.OverlapCircle(CeilingCheck.position, 0.2f,ground)) 
        { 
            if (Input.GetButton("Crouch"))
            {
                capColl.size = colliderCrouchSize;
                capColl.offset = colliderCrouchOffset;
                boxColl.enabled = false;
                anim.SetBool("crouching", true);
                //DisColl.enabled = false;
            }
            else 
            {
                capColl.size = colliderStandSize;
                capColl.offset = colliderStandOffset;
                boxColl.enabled = true;
                anim.SetBool("crouching", false);
                //DisColl.enabled = true;
            }
        }
    }
    //�ؿ���Ϸ
    void Restart()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    //�ɶ������Ծ
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

