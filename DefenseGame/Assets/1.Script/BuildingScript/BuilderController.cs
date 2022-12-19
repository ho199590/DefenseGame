using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderController : MonoBehaviour
{
    #region 변수
    [HideInInspector]
    public bool canBuild = true;
    [HideInInspector]
    public bool HasMoney { get { return PlayerController.money >= cost; } }
    public static BuilderController instance;
    private GameObject towerToBuild;
    private GameObject towerToAction;
    [SerializeField]
    private GameObject[] towerList;

    public PlayerOperate playerOP;
    [HideInInspector]
    public int cost = 10;
    int code;

    [SerializeField]
    GameObject buildParticle;
    [SerializeField]
    GameObject upgradeParticle;

    [SerializeField]
    TowerScriptable towerContainer;
    #endregion

    #region 함수
    private void Awake()
    {
        if (instance != null)
        {
            print("두개 이상의 빌더를 컨트롤 할 수 없습니다.");
            return;
        }
        instance = this;
    }

    public bool CanBuild { get { return towerToBuild != null; } }

    public void BuildTowerOn(NodeScript node)
    {
        if (PlayerController.money < cost) { print("Not Enought Money"); return; }
        PlayerController.money -= cost;
        // 건설 관련 스크립트
        (int, GameObject) towerParam = GetTowerToBuild();

        int code = towerParam.Item1;
        GameObject towerTobuild = towerParam.Item2;

        GameObject tower = Instantiate(towerTobuild, node.getPostionOffset(), Quaternion.identity);
        tower.GetComponent<TowerController>().towerCode = code;
        tower.GetComponent<TowerController>().SetState();

        var effect = Instantiate(buildParticle, node.getPostionOffset(), Quaternion.identity);


        Destroy(effect, 2f);
        node.tower = tower;
    }


    public (int, GameObject) GetTowerToBuild()
    {
        int num = Random.Range(0, towerList.Length);
        GameObject tower = towerList[num];

        code = num;
        towerToBuild = tower;

        return (code, towerToBuild);
    }

    public void SetTowerToAction(GameObject tower)
    {
        towerToAction = tower;
    }
    #endregion
}
