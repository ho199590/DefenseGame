using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHandler : MonoBehaviour
{
    #region 변수

    public int level;
    public float lifeTime;

    private Transform target;
    private int pointIndex = 0;


    // 파라미터
    EnemyParam enemy;
    private int attack;
    private float health;
    private float speed;
    private float guard;
    [SerializeField]
    EnemyScriptable enemyContainer;
    #endregion
    #region 함수
    private void Start()
    {
        target = WayPointHandler.points[0];

        level = FindObjectOfType<EnemySpawnController>().waveLevel;

        enemy = enemyContainer.GetEnemyList(level % 2);
        attack = enemy.attack;
        health = enemy.health;
        speed = enemy.speed;
        guard = enemy.armor;
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        lifeTime += Time.deltaTime;

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
        target = WayPointHandler.points[pointIndex];
        pointIndex++;
    }
    #endregion
}
