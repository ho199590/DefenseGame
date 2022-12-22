using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeUIScript : MonoBehaviour
{
    #region ����
    [SerializeField]
    Vector3 offset;
    private NodeScript target;
    public GameObject UI;
    #endregion

    #region �Լ�
    public void SetTarget(NodeScript node)
    {   
        target = node;
        transform.position = target.getPostionOffset() + offset;

        print("�ڵ� : " + target.tower.GetComponent<TowerController>().towerCode);

        if (!UI.active)
        {   
            UI.transform.DOScale(Vector3.one * 0.07f, 0.3f).From(Vector3.zero);
        }
        UI.SetActive(true);
    }

    public void Hide()
    {
        UI.SetActive(false);
    }

    public void TowerUpgrade()
    {
        target.Upgrade();
    }

    public void TowerSell()
    {
        target.SellTower();
    }

    public void TowerMove()
    {
        target.TowerMove();
    }
    #endregion
}
