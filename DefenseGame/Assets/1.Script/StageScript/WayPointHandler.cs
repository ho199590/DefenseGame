using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointHandler : MonoBehaviour
{
    #region ����
    public static Transform[] points;
    #endregion

    #region �Լ�
    private void Awake()
    {
        points = new Transform[transform.childCount];

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }
    #endregion
}
