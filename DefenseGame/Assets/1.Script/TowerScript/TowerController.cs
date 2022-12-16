using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    #region 변수    
    private Transform target;    
    public int towerCode;
    public int towerLevel;
    // 적 찾기 및 회전
    public string enemyTag = "Enemy";
    public Transform firePoint;
    public Transform partToRotate;
    private float turnSpeed = 10f;    
    private float shotCountdown = 0f;

    [SerializeField]
    TowerScriptable towerState;
    private TowerClass tower;


    private float attack;    
    private float speed;    
    private float range;    
    private bool skill;
    private GameObject bullet;
    #endregion

    #region 함수
    #region 타워 스탯 가져오기
    private void OnEnable()
    {
        GetState();
    }
    public void GetState()
    {
        tower = towerState.GetTowerState(towerCode);

        attack = tower.level[towerLevel].attack;
        speed = tower.level[towerLevel].speed;
        range = tower.level[towerLevel].range;
        skill = tower.level[towerLevel].skill;
        bullet = tower.level[towerLevel].bullet;
    }
    #endregion
    #region 타겟 설정 관련 함수
    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0, 0.5f);
    }
    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= range)
        {     
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }
    #endregion
    #region 사격 관련
    void Shot()
    {
        print("Shot");
        var shotBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
        shotBullet.GetComponent<BulletHandler>().SetTarget(target);
    }
    #endregion


    private void Update()
    {
        if (target == null)
        {
            return;
        }
        //포탑 회전 관련
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0);

        //사격 관련
        if(shotCountdown <= 0f)
        {
            Shot();
            shotCountdown = 1f / speed;
        }
        shotCountdown -= Time.deltaTime;

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    #endregion
}

