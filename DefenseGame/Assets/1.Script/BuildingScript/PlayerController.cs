using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region ����
    public static int money;
    public static int life;
    public int startMony = 100;
    public int startLife = 10;
    #endregion

    #region �Լ�
    private void Start()
    {
        money = startMony;
        life = startLife;
    }
    #endregion
}
