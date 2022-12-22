using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BluePrint{
    public int cost;
    public PlayerOperate operate;
}

public enum PlayerOperate
{
    Build,
    Destroy,
    Move,
    Upgrade
}

public class CurrencyHandler : MonoBehaviour
{
    #region º¯¼ö

    public Text moneyText;
    public Text lifeText;

    BuilderController builder;

    public BluePrint randomTower;
    public BluePrint towerMove;
    public BluePrint towerUpgrade;
    public BluePrint towerDestroy;

    #endregion

    #region
    public void PurchaseStandard()
    {

    }

    private void Update()
    {
        moneyText.text = PlayerController.money.ToString();
        lifeText.text = PlayerController.life.ToString();
    }

    #endregion
}
