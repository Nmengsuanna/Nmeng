using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collection : MonoBehaviour
{
    //获取收集品的组件
    //动画
    protected Animator anim;
    //音源
    protected AudioSource collectedAudio;



    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        collectedAudio = GetComponent<AudioSource>();

    }
    //移除收集物
    public void Remove()
    {
        Destroy(gameObject);
    }
    //收集音乐与动画
    public void IsCollected()
    {
        AudioManager.instance.CollectedAudio();
        anim.SetTrigger("Is_Collected");
    }
    //隐藏
    public void Hide()
    {
        transform.position = new Vector3(0, 0, -20);
    }
}
