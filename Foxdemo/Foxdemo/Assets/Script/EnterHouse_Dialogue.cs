using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterHouse_Dialogue : MonoBehaviour
{
    //���Dialogue���������
    public GameObject enterDialogue;
    //������ײ������Ի���
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            enterDialogue.SetActive(true);
        }
    }
    //�뿪���رնԻ���
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            enterDialogue.SetActive(false);
        }
    }
}
