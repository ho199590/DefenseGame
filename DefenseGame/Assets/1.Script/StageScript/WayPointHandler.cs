using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointHandler : MonoBehaviour
{
    #region 변수
    public static Transform[] points;
    #endregion

    #region 함수
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
