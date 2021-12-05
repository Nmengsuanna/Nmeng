using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform Cam;//获得摄像头的坐标，他会随着当前的位置改变
    public float movementRate;//设置相对速度比率
    private float startPointX,startPointY;//获取开始的摄像头点位
    public bool lockY;//false//对于Y是否要有类似效果

    void Start()
    {
        startPointX = transform.position.x;
        startPointY = transform.position.y;
    }

    void Update()
    {
        if(lockY)
        transform.position = new Vector2(startPointX + Cam.position.x * movementRate, transform.position.y);
        //设置运动速率
        else
        {
            transform.position = new Vector2(startPointX + Cam.position.x * movementRate, startPointY+Cam.position.y*movementRate);
        }
    }
}
