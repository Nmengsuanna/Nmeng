using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform Cam;//�������ͷ�����꣬�������ŵ�ǰ��λ�øı�
    public float movementRate;//��������ٶȱ���
    private float startPointX,startPointY;//��ȡ��ʼ������ͷ��λ
    public bool lockY;//false//����Y�Ƿ�Ҫ������Ч��

    void Start()
    {
        startPointX = transform.position.x;
        startPointY = transform.position.y;
    }

    void Update()
    {
        if(lockY)
        transform.position = new Vector2(startPointX + Cam.position.x * movementRate, transform.position.y);
        //�����˶�����
        else
        {
            transform.position = new Vector2(startPointX + Cam.position.x * movementRate, startPointY+Cam.position.y*movementRate);
        }
    }
}
