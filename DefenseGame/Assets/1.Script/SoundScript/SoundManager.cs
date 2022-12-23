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
    #region 변수
    public static SoundManager instance;
    [SerializeField]
    List<SoundParam> soundParams = new List<SoundParam>();

    AudioSource bgm;
    [SerializeField]
    AudioClip[] bgms;

    private int curBgm = 0;
    #endregion

    #region 함수
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        bgm = GetComponent<AudioSource>();
    }

    public void SoundOnShot(AudioClip clip)
    {
        bgm.PlayOneShot(clip);
    }

    public void SoundByNum(int num)
    {
        bgm.PlayOneShot(soundParams[num].clip);
    }

    public void SetBgm(int num)
    {
        bgm.clip = bgms[num];
        bgm.Play();
    }

    public void SetNextBgm()
    {
        curBgm++;
        bgm.clip = bgms[curBgm];
        bgm.Play();
    }
    #region 테스트용 임시 함수
    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.M))
        {
            SetBgm(testNum);
        }
        */
    }
    #endregion
    #endregion
}
