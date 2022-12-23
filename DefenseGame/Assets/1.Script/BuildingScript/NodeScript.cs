using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodeScript : MonoBehaviour
{
    #region 변수
    public Vector3 positionOffset;

    [SerializeField]
    private Color hoverColor;
    private Color startColor;
    [SerializeField]
    private Color enoughtColor;

    [SerializeField]
    public GameObject tower;
    private Renderer render;

    #endregion

    #region 함수
    private void Start()
    {
        render = GetComponent<Renderer>();
        startColor = render.material.color;
    }

#if PLATFORM_STANDALONE_WIN
    private void OnMouseEnter()
    {
        if (BuilderController.instance.HasMoney)
        {
            render.material.color = hoverColor;
        }
        else
        {
            render.material.color = enoughtColor;
        }
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())return;


        if (tower != null)
        {
            BuilderController.instance.SelectNode(this);

            if (BuilderController.instance.playerOP == PlayerOperate.Upgrade)
            {
                if(tower.GetComponent<TowerController>().towerLevel < 4)
                {
                    if (BuilderController.instance.GetTowerToAction() != null)
                    {
                        MergeTower();
                    }
                    else
                    {
                        BuilderController.instance.SetTowerToAction(tower);
                    }
                }
                else
                {
                    GameManager.instance.AlterPopup(2);
                }

                BuilderController.instance.DeselectNode();
            }

            if (BuilderController.instance.playerOP == PlayerOperate.Move)
            {
                print("Action Node Select");
                BuilderController.instance.SetNodeToAction(this);
            }
            return;
        }


        if (BuilderController.instance.playerOP == PlayerOperate.Move)
        {   
            if (BuilderController.instance.GetNodeToAction() != null)
            {   
                TowerMove();
            }

            BuilderController.instance.DeselectNode();
        }

        if (BuilderController.instance.playerOP == PlayerOperate.Build)
        {
            BuildTower();
        }

    }
    public void BuildTower()
    {
        if (tower != null)
        {
            GameManager.instance.AlterPopup(1);
            return;
        }

        BuilderController.instance.BuildTowerOn(transform.GetComponent<NodeScript>());
    }

    private void OnMouseExit()
    {
        render.material.color = startColor;
    }
    #region 타워 업그레이드 관련
    public void Upgrade()
    {
        TowerController towerCon = tower.GetComponent<TowerController>();

        if(towerCon.towerLevel < 4)
        {
            int cost = (int)Mathf.Pow(2, (towerCon.towerLevel + 1)) * 10;            
            if (PlayerController.money >= cost)
            {
                towerCon.towerLevel += 1;                
                towerCon.SetState();                
                PlayerController.money -= cost;
                BuilderController.instance.UpgradeSelect();

                SoundManager.instance.SoundByNum(6);
            }
            else
            {
                GameManager.instance.AlterPopup(0);
            }
        }
        else
        {
            GameManager.instance.AlterPopup(2);
        }

        BuilderController.instance.DeselectNode();
    }

    
    public void MergeTower()
    {
        TowerController targetTower = tower.GetComponent<TowerController>();
        TowerController sourceTower = BuilderController.instance.GetTowerToAction().GetComponent<TowerController>();


        if (targetTower == sourceTower)
        {
            GameManager.instance.AlterPopup(3);
            return;
        }
        if(targetTower.towerLevel == sourceTower.towerLevel && targetTower.towerCode == sourceTower.towerCode)
        {
            sourceTower.transform.DOMove(targetTower.transform.position , 1f).From(sourceTower.transform.position).SetEase(Ease.OutExpo).OnComplete(() =>
            {
                targetTower.towerLevel += 1;
                targetTower.SetState();

                BuilderController.instance.MergeParticle(this);
                BuilderController.instance.SetTowerToAction(null);

                Destroy(sourceTower.gameObject);

                SoundManager.instance.SoundByNum(7);

            });
        }

    }
    #endregion
    #region 타워 판매 관련
    public void SellTower()
    {
        TowerController towerCon = tower.GetComponent<TowerController>();
        int cost = (int)Mathf.Pow(2, (towerCon.towerLevel)) * 5;

        PlayerController.money += cost;
        
        TowerCleaner();

        BuilderController.instance.SelledParticle(this);
        BuilderController.instance.DeselectNode();

        SoundManager.instance.SoundByNum(3);
    }
    public void TowerCleaner()
    {
        Destroy(tower);
        tower = null;
    }
    #endregion
    #region 타워 이동 관련
    public void TowerMove()
    {
        if(tower == null)
        {
            if(tower == BuilderController.instance.GetNodeToAction().tower)
            {
                return;
            }

            if(PlayerController.money < 50)
            {
                GameManager.instance.AlterPopup(0);
                return;
            }
            else
            {
                //tower = BuilderController.instance.GetNodeToAction().tower;
                //BuilderController.instance.GetNodeToAction().TowerCleaner();

                BuilderController.instance.MoveTowerOn(this);
            }
        }
        else
        {
            GameManager.instance.AlterPopup(1);
        }
    }
    #endregion
    public Vector3 getPostionOffset() { return transform.position + positionOffset; }
#endif
    #endregion
}
