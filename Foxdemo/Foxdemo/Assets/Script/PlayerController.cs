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
    //
    public LayerMask ground;
    //
    public Collider2D coll;
    //
    public int Cherry = 0;
    //ӣ��UI��Ŀ
    public Text CherryNum;
    //�Ƿ�����
    private bool isHurt;
    //������Դ
    public AudioSource jumpAudio;
    //�¶׹رյ���ײ��
    public Collider2D DisColl;
    //�ж��Ƿ�ͷ
    public Transform CeilingCheck;



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
       //��ɫ��Ծ
        if(Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            jumpAudio.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            anim.SetBool("jumping", true);
         
        }
    }
    
    //�ı䶯��
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
    //��ײ������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collection")
        {
            Destroy(collision.gameObject);
            Cherry += 1;
            CherryNum.text = Cherry.ToString();
        }
        
        if(collision.tag == "DeadLine")
        {
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
} 

