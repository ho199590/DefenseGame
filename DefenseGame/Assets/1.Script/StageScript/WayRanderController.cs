using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayRanderController : MonoBehaviour
{
    #region ����
    [Header("���׸���")]
    [SerializeField]
    Material[] skins;

    int curMat = 0;

    private Renderer[] ways;
    #endregion

    #region �Լ�
    private void Start()
    {
        ways = transform.GetComponentsInChildren<Renderer>();

        SetMaterial(0);
    }

    public void SetMaterial(int num)
    {
        foreach(Renderer r in ways)
        {
            r.material = skins[num];
        }

        curMat = num;
    }

    public void SetNextMaterial()
    {
        curMat++;
        foreach (Renderer r in ways)
        {
            r.material = skins[curMat];
        }
    }
    #endregion
}
