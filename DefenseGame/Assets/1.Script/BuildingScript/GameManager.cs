using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region 변수
    public static GameManager instance;

    private static bool gameEnd = false;
    public static int gameLevel = 0;
    public int grade = 1;

    [Header("게임 진행 UI")]
    public Text wavelevel;
    [SerializeField]
    GameObject AlterBox;
    [SerializeField]
    Text AlterMassage;
    [SerializeField]
    string[] alterMassages;

    [Header("오버레이 UI")]
    public GameObject overlayCanvas;
    public GameObject gameOverUI;
    #endregion

    #region 함수
    private void Awake()
    {
        if (instance != null)
        {
            print("게임 매니저가 이미 존재합니다.");
            return;
        }
        instance = this;

    }
    private void Start()
    {
        AlterBox.SetActive(false);
        gameEnd = false;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            SoundManager.instance.SoundByNum(2);

            EndGame();
        }

        if (gameEnd) { return; }
        if (PlayerController.life <= 0)
        {
            SoundManager.instance.SoundByNum(2);
            EndGame();
        }
        wavelevel.text = gameLevel.ToString();
    }

    void EndGame()
    {
        gameEnd = true;
        //overlayCanvas.SetActive(true);
        gameOverUI.SetActive(true);
    }

    public void AlterPopup(int num)
    {
        AlterBox.SetActive(false);
        AlterMassage.text = alterMassages[num];
        AlterBox.SetActive(true);              

        if(num != 4)
        {
            SoundManager.instance.SoundByNum(5);
        }
    }
    #endregion
}
