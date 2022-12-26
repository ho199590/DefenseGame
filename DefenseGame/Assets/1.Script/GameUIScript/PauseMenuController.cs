using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    #region ����
    public GameObject UI;
    public Text waveCount;

    [SerializeField]
    SceneFader sceneFader;

    [SerializeField]
    CameraController cc;
    #endregion

    #region �Լ�
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        waveCount.text = "���� ���̺� : " + GameManager.gameLevel.ToString();
        UI.SetActive(!UI.activeSelf);

        if (UI.activeSelf)
        {
            SoundManager.instance.SoundByNum(0);

            cc.doMovement = false;

            Time.timeScale = 0;
        }
        else
        {
            SoundManager.instance.SoundByNum(1);

            Time.timeScale = 1;

            cc.doMovement = true;
        }
    }

    public void Retry()
    {
        Toggle();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {

        sceneFader.FadeTo("MainMenu");
    }
    #endregion
}
