using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderController : MonoBehaviour
{
    #region 변수
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

    #region 함수
    private void Awake()
    {
        if(instance != null)
        {
            print("두개 이상의 빌더를 컨트롤 할 수 없습니다.");
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
