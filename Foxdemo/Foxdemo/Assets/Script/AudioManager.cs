using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //ʵ��������
    public static AudioManager instance;
    //�������
    public AudioSource audioSource;
    [SerializeField]
    private AudioClip jumpAudio, hurtAudio, collectedAudio, endAudio, deadAudio;

    //�ű���ʱ��ʵ�����Ķ���������ı���
    private void Awake()
    {
        instance = this;
    }
    //��Ծ
    public void JumpAudio()
    {
        audioSource.clip = jumpAudio;
        audioSource.Play();
    }
    //����
    public void HurtAudio()
    {
        audioSource.clip = hurtAudio;
        audioSource.Play();
    }
    //�ռ���Ʒ
    public void CollectedAudio()
    {
        audioSource.clip = collectedAudio;
        audioSource.Play();
    }
    //��Ϸ����
    public void EndAudio()
    {
        audioSource.clip = endAudio;
        audioSource.Play();
    }
    //�������
    public void DeadAudio()
    {
        audioSource.clip = deadAudio;
        audioSource.Play();
    }
   
}
