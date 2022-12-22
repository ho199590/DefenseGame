using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    #region 변수
    [Header("UI 오브젝트")]
    [SerializeField]
    Image backGround;
    [SerializeField]
    Transform gameOverText;
    [SerializeField]
    Transform resultUI;
    [SerializeField]
    Transform UIButton;
    [SerializeField]
    Transform resetButton;
    [SerializeField]
    Transform MenuButton;
    [SerializeField]
    Text waveScore, roundText;

    [Header("등록 포지션")]
    [SerializeField]
    Transform[] pos;

    [SerializeField]
    SceneFader sceneFader;

    #endregion

    #region 함수
    private void OnEnable()
    {
        backGround.DOFade(0.5f, 0.3f).From(0);

        UIButton.gameObject.SetActive(false);
        SetWaveScore();
        ShowAnim();
    }

    public void ShowAnim()
    {        
        gameOverText.DOMove(pos[1].position, 1f).From(pos[0].position);
        gameOverText.GetComponent<Text>().DOFade(1, 1).From(0);
        resultUI.DOScale(Vector3.one, 0.5f).From(Vector3.zero).SetDelay(1f).OnComplete(() => { UIButton.gameObject.SetActive(true); });
    }

    public void SetWaveScore()
    {
        waveScore.text = GameManager.gameLevel.ToString();
    }

    public void Retry()
    {
        GameManager.gameLevel = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        GameManager.gameLevel = 0;
        sceneFader.FadeTo("MainMenu");
    }
    #endregion
}
