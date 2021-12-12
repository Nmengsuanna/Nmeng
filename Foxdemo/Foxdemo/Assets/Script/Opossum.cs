using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opossum : Enemy
{
    public Transform leftPoint, rightPoint;
    private float leftx, rightx;
    private bool faceleft = true;
    private float speed = 6;
    
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();

        leftx = leftPoint.position.x;
        rightx = rightPoint.position.x;

    }

    
    void Update()
    {
        if (jumpOn == false)
        {
            Movement();
        }
    }
    void Movement()
    {
        if (faceleft)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            if (rb.position.x < leftx)
            {
                rb.transform.localScale = new Vector3(-1, 1, 1);
                faceleft = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            if (rb.position.x > rightx)
            {
                rb.transform.localScale = new Vector3(1, 1, 1);
                faceleft = true;
            }
        }
    }
}
