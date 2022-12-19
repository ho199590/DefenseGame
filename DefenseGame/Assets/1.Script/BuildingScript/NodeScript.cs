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


        if (tower != null)
        {
            BuilderController.instance.SetTowerToAction(tower);
        }
    }

    private void OnMouseDown()
    {

        if (BuilderController.instance.playerOP == PlayerOperate.Build)
        {
            BuildTower();
        }
        if (BuilderController.instance.playerOP == PlayerOperate.Upgrade)
        {
            print("Upgrade");
        }
        if (BuilderController.instance.playerOP == PlayerOperate.Destroy)
        {
            print("Destroy");
        }
        if (BuilderController.instance.playerOP == PlayerOperate.Move)
        {
            print("Move");
        }

    }

    public void BuildTower()
    {
        if (tower != null)
        {
            print("현제 위치에 건설 불가");
            return;
        }

        BuilderController.instance.BuildTowerOn(transform.GetComponent<NodeScript>());
    }

    private void OnMouseExit()
    {
        render.material.color = startColor;
        if (tower != null)
        {
            BuilderController.instance.SetTowerToAction(null);
        }
    }

    public Vector3 getPostionOffset()
    {
        return transform.position + positionOffset;
    }
#endif
    #endregion
}
