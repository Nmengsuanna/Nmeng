using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        //角色移动
        if (Horizontalmove != 0) {
            rb.velocity = new Vector2(Horizontalmove * speed * Time.fixedDeltaTime, rb.velocity.y);
            //角色移动时的动作
            anim.SetFloat("running", Mathf.Abs(facedirection));
        }
        if (facedirection !=0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);

        }
       //角色跳跃
        if(Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.fixedDeltaTime);
         
        }
    }

    
}
