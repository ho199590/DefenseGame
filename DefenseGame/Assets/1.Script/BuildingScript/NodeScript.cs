using UnityEngine;

public class NodeScript : MonoBehaviour
{
    #region 변수
    public Vector3 positionOffset;

    [SerializeField]
    private Color hoverColor;
    private Color startColor;

    [SerializeField]
    private GameObject tower;
    private Renderer render;

    #endregion

    #region 함수
    private void Start()
    {
        render = GetComponent<Renderer>();
        startColor = render.material.color;
    }

#if PLATFORM_STANDALONE_WIN
    private void OnMouseOver()
    {
        render.material.color = hoverColor;
    }

    private void OnMouseDown()
    {
        if(tower != null)
        {
            print("현제 위치에 건설 불가");
            return;
        }

        // 건설 관련 스크립트

        int code = BuilderController.instance.GetTowerToBuild().Item1;
        GameObject towerTobuild = BuilderController.instance.GetTowerToBuild().Item2;
        tower = Instantiate(towerTobuild, transform.position + positionOffset, transform.rotation);
        tower.GetComponent<TowerController>().towerCode = code;
        tower.GetComponent<TowerController>().GetState();
    }

    private void OnMouseExit()
    {
        render.material.color = startColor;
    }
#endif
    #endregion
}
