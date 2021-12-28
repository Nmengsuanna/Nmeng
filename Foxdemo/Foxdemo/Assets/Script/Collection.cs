using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collection : MonoBehaviour
{
    //��ȡ�ռ�Ʒ�����
    //����
    protected Animator anim;
    //��Դ
    protected AudioSource collectedAudio;



    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        collectedAudio = GetComponent<AudioSource>();

    }
    //�Ƴ��ռ���
    public void Remove()
    {
        Destroy(gameObject);
    }
    //�ռ������붯��
    public void IsCollected()
    {
        AudioManager.instance.CollectedAudio();
        anim.SetTrigger("Is_Collected");
    }
    //����
    public void Hide()
    {
        transform.position = new Vector3(0, 0, -20);
    }
}
