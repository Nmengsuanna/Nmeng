using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterHouse_Dialogue : MonoBehaviour
{
    //获得Dialogue的所有组件
    public GameObject enterDialogue;
    //触发碰撞，激活对话框
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            enterDialogue.SetActive(true);
        }
    }
    //离开，关闭对话框
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            enterDialogue.SetActive(false);
        }
    }
}
