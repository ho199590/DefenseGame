using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour
{
    #region 변수
    [SerializeField]
    GameObject mouseEffect;
    [SerializeField]
    GameObject ClickEffect;
    [SerializeField]
    LayerMask catchLayer;

    Ray ray;
    RaycastHit hit;


    [SerializeField]
    SceneFader sceneFader;

    [SerializeField]
    AudioClip startButton;
    #endregion

    #region 함수
    private void Start()
    {
        hit = new RaycastHit();

    }

    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, catchLayer))
        {
            mouseEffect.transform.position = hit.point;
        }
    }

    public void ChangeScene()
    {
        SoundManager.instance.SoundOnShot(startButton);
        sceneFader.FadeTo("Stage");
    }
    #endregion

}
