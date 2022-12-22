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
    private NodeScript selectedNode;
    private NodeScript actionNode;
    [SerializeField]
    private GameObject[] towerList;

    public PlayerOperate playerOP;
    [HideInInspector]
    public int cost = 10;
    int code;

    [Header("파티클")]
    [SerializeField]
    NodeUIScript nodeUI;
    [SerializeField]
    GameObject buildParticle;
    [SerializeField]
    GameObject upgradeParticle;
    [SerializeField]
    GameObject mergeParticle;
    [SerializeField]
    GameObject sellParticle;

    [SerializeField]
    TowerScriptable towerContainer;
    public bool CanBuild { get { return towerToBuild != null; } }
    
    #endregion

    #region 함수
    private void Awake()
    {
        cost = 10;
        if (instance != null)
        {
            print("두개 이상의 빌더를 컨트롤 할 수 없습니다.");
            return;
        }
        instance = this;
        
    }


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
        tower.GetComponent<TowerController>().SetMyNode(node);
    }


    public (int, GameObject) GetTowerToBuild()
    {
        int num = Random.Range(0, 100);
        code = RandomNumber(num);

        GameObject tower = towerList[code];
        towerToBuild = tower;

        DeselectNode();
        return (code, towerToBuild);
    }

    public int RandomNumber(int num)
    {
        int result = num switch
        {
            > 35 and <= 55 => 1,
            > 55 and <= 85 => 2,
            > 85 and <= 90 => 3,
            > 90 => 4,
            _ => 0
        };
        return result;
    }

    public void SetTowerToAction(GameObject tower)
    {
        towerToAction = tower;
        //actionNode = 
    }

    public GameObject GetTowerToAction()
    {
        return towerToAction;
    }

    public void SelectNode(NodeScript node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
        }

        selectedNode = node;
        towerToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    #region 노드 파티클
    public void UpgradeSelect()
    {
        Instantiate(upgradeParticle, selectedNode.getPostionOffset(), Quaternion.identity);
    }

    public void MergeParticle(NodeScript node)
    {
        Instantiate(mergeParticle, node.getPostionOffset(), Quaternion.identity);
    }

    public void SelledParticle(NodeScript node)
    {
        Instantiate(sellParticle, node.getPostionOffset(), Quaternion.identity);
    }
    #endregion
    #endregion
}
