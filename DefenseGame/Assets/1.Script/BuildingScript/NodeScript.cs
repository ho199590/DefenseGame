using UnityEngine;

public class NodeScript : MonoBehaviour
{
    #region ����
    public Vector3 positionOffset;

    [SerializeField]
    private Color hoverColor;
    private Color startColor;

    [SerializeField]
    private GameObject tower;
    private Renderer render;

    #endregion

    #region �Լ�
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
            print("���� ��ġ�� �Ǽ� �Ұ�");
            return;
        }

        // �Ǽ� ���� ��ũ��Ʈ        
        (int, GameObject) towerParam = BuilderController.instance.GetTowerToBuild();
        int code = towerParam.Item1;
        GameObject towerTobuild = towerParam.Item2;
        tower = Instantiate(towerTobuild, transform.position + positionOffset, transform.rotation);
        tower.GetComponent<TowerController>().towerCode = code;
        tower.GetComponent<TowerController>().SetState();
    }

    private void OnMouseExit()
    {
        render.material.color = startColor;
    }
#endif
    #endregion
}
