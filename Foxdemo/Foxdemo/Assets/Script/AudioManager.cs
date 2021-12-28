using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //实例化对象
    public static AudioManager instance;
    //声音组件
    public AudioSource audioSource;
    [SerializeField]
    private AudioClip jumpAudio, hurtAudio, collectedAudio, endAudio, deadAudio;

    //脚本打开时让实例化的对象等于他的本生
    private void Awake()
    {
        instance = this;
    }
    //跳跃
    public void JumpAudio()
    {
        audioSource.clip = jumpAudio;
        audioSource.Play();
    }
    //受伤
    public void HurtAudio()
    {
        audioSource.clip = hurtAudio;
        audioSource.Play();
    }
    //收集物品
    public void CollectedAudio()
    {
        audioSource.clip = collectedAudio;
        audioSource.Play();
    }
    //游戏结束
    public void EndAudio()
    {
        audioSource.clip = endAudio;
        audioSource.Play();
    }
    //消灭怪物
    public void DeadAudio()
    {
        audioSource.clip = deadAudio;
        audioSource.Play();
    }
   
}
