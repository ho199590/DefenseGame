using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
class SoundParam
{
    public string name;
    public AudioClip clip;
}


public class SoundManager : MonoBehaviour
{
    #region ����
    public static SoundManager instance;
    [SerializeField]
    List<SoundParam> soundParams = new List<SoundParam>();

    AudioSource audio;
    #endregion

    #region �Լ�
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        audio = GetComponent<AudioSource>();
    }

    public void SoundOnShot(AudioClip clip)
    {
        audio.PlayOneShot(clip);
    }

    public void SoundByNum(int num)
    {
        audio.PlayOneShot(soundParams[num].clip);
    }
    #endregion
}
