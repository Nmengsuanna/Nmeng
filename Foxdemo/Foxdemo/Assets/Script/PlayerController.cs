using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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




    // Start is called before the first frame  update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        SwitchAnim();
    }
    void Movement()
    {
        float Horizontalmove;
        float facedirection;


        Horizontalmove = Input.GetAxis("Horizontal");
        facedirection = Input.GetAxisRaw("Horizontal");

        //��ɫ�ƶ�
        if (Horizontalmove != 0) {
            rb.velocity = new Vector2(Horizontalmove * speed , rb.velocity.y);
            //��ɫ�ƶ�ʱ�Ķ���
            anim.SetFloat("running", Mathf.Abs(facedirection));
        }
        if (facedirection !=0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);

        }
       //��ɫ��Ծ
        if(Input.GetButtonDown("Jump"))
        {
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
        }else if (coll.IsTouchingLayers(ground))
            {
                anim.SetBool("falling", false);
                anim.SetBool("idle", true);
            }
    }
}
