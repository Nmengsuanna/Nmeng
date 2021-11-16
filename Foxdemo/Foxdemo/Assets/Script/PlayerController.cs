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



    // Start is called before the first frame  update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
    }
    void Movement()
    {
        float Horizontalmove;
        float facedirection;


        Horizontalmove = Input.GetAxis("Horizontal");
        facedirection = Input.GetAxisRaw("Horizontal");

        //��ɫ�ƶ�
        if (Horizontalmove != 0) {
            rb.velocity = new Vector2(Horizontalmove * speed * Time.fixedDeltaTime, rb.velocity.y);
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
            rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.fixedDeltaTime);
         
        }
    }

    
}
