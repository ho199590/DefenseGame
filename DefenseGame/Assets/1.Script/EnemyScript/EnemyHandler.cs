using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHandler : MonoBehaviour
{
    #region 변수

    // 파라미터
    public int attack;
    public float health;
    public float speed;
    public float guard;


    private Transform target;
    private int pointIndex = 0;
    #endregion
    #region 함수
    private void Start()
    {
        target = WayPointHandler.points[0];

    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if(Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWayPoint();
        }
    }

    void GetNextWayPoint()
    {
        if(pointIndex >= WayPointHandler.points.Length)
        {
            Destroy(gameObject);
            return;
        }
        pointIndex++;
        target = WayPointHandler.points[pointIndex];
    }
    #endregion
}
