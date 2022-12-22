using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    #region 변수
    public GameObject UI;
    public Text waveCount;
    #endregion

    #region 함수
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        waveCount.text = "현재 웨이브 : " + GameManager.gameLevel.ToString();
        UI.SetActive(!UI.activeSelf);

        if (UI.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void Retry()
    {
        Toggle();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        print("MENU OPEN");
    }
    #endregion
}
