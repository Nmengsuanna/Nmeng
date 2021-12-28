using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //�����������
    public Rigidbody2D rb;
    //�����ٶ�
    private float speed;
    //������Ծ����
    public float jumpforce;
    //�������������
    public Animator anim;
    //���õ���
    public LayerMask ground;
    //��ײ��
    private CapsuleCollider2D capColl;
    private Collider2D edgColl;
    //ӣ����Ŀ
    public int Cherry = 0;
    public int Gem = 0;
    //ӣ��UI��Ŀ
    //public Text CherryNum;
    //�Ƿ�����
    private bool isHurt;
    //������Դ
    public AudioSource jumpAudio;
    public AudioSource hurtAudio;
    public AudioSource deadAudio;
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
    //�ж��Ƿ�������
    private bool falling = false;
    //��ý�ɫ��λ
    private Transform playerTransform;
    //ӣ������UI
    public TMP_Text CherryNum;
    //��ʯ����UI
    public TMP_Text GemNum;
    //����ֵ
    public int Health = 3;
    //����������
    private float mobKillpoint;
    //



    void Start()
    {
        capColl = GetComponent<CapsuleCollider2D>();
        colliderStandSize = capColl.size;
        colliderStandOffset = capColl.offset;
        colliderCrouchSize = new Vector2(capColl.size.x, capColl.size.y * 0.5f);
        colliderCrouchOffset = new Vector2(capColl.offset.x, capColl.offset.y * 1.5f);
        playerTransform = GetComponent<Transform>();
        speed = 8f;
        mobKillpoint = GroundCheck.position.y;

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
    //��ײ������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //��Ʒ�ռ�
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
        //�����ؿ�
        if (collision.tag == "DeadLine")
        {
            deadAudio.Play();
            death = true;
            rb.velocity = new Vector2(0, 0);
            GetComponent<AudioSource>().enabled = false;
            Invoke("Restart", 3f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�йص���
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy  = collision.gameObject.GetComponent<Enemy>();
            //�������
            if (falling == true && rb.position.y > enemy.transform.position.y)
            {
                enemy.JumpOn();
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                anim.SetBool("jumping", true);
            }
            //�����������˺󷴵�
            else if (transform.position.x < collision.gameObject.transform.position.x)
            {
                AudioManager.instance.HurtAudio();
                isHurt = true;
                rb.velocity = new Vector2(-10f, rb.velocity.y);
                anim.SetBool("hurt", true);
                HealthDeath();
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                AudioManager.instance.HurtAudio();
                isHurt = true;
                rb.velocity = new Vector2(10f, rb.velocity.y);
                anim.SetBool("hurt", true);
                HealthDeath();
            }
        }     
    }
    //�¶�
    void Crouch()
    {
        //���ͷ��
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
            anim.SetBool("falling", false);
            AudioManager.instance.JumpAudio();
        }
    }
    void HealthDeath()
    {
        Health --;
        if (Health == 2)
        {
            GameObject.Find("UI/Heart2").SetActive(false);
        }
        else if (Health == 1)
        {
            GameObject.Find("UI/Heart1").SetActive(false);
        }
        else if (Health == 0)
        {
            GetComponent<AudioSource>().enabled = false;
            AudioManager.instance.EndAudio();
            GameObject.Find("UI/Heart").SetActive(false);
            rb.velocity = new Vector2(0.2f, 16f);
            gameObject.GetComponent<EdgeCollider2D>().enabled = false;
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            Invoke("Restart", 4f);
        }
    }
} 

