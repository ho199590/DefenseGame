using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderController : MonoBehaviour
{
    #region ����
    public bool canBuild = true;
    public static BuilderController instance;
    private GameObject towerToBuild;
    [SerializeField]
    private GameObject[] towerList;


    [SerializeField]
    int code;

    [SerializeField]
    TowerScriptable towerContainer;
    #endregion

    #region �Լ�
    private void Awake()
    {
        if(instance != null)
        {
            print("�ΰ� �̻��� ������ ��Ʈ�� �� �� �����ϴ�.");
            return;
        }
        instance = this;
    }

    public (int , GameObject) GetTowerToBuild()
    {
        int num = Random.Range(0, towerList.Length);
        GameObject tower = towerList[num];
        
        code = num;        
        towerToBuild = tower;

        return (code, towerToBuild);
    }

    #endregion
}
