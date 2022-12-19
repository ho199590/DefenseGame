using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    #region ����    
    private Transform target;    
    public int towerCode;
    public int towerLevel;
    // �� ã�� �� ȸ��
    public string enemyTag = "Enemy";
    public Transform firePoint;
    public Transform partToRotate;
    private float turnSpeed = 10f;    
    private float shotCountdown = 0f;

    [SerializeField]
    TowerScriptable towerState;
    private TowerClass tower;


    private float attack;
    [SerializeField]
    private float speed;    
    private float range;    
    private bool skill;
    private GameObject bullet;
    #endregion

    #region �Լ�
    #region Ÿ�� ���� ��������
    private void OnEnable()
    {
        SetState();
    }
    public void SetState()
    {
        tower = towerState.GetTowerState(towerCode);

        attack = tower.level[towerLevel].attack;
        speed = tower.level[towerLevel].speed;
        range = tower.level[towerLevel].range;
        skill = tower.level[towerLevel].skill;
        bullet = tower.level[towerLevel].bullet;

        SetModel();
        partToRotate = transform.GetChild(towerLevel);
    }

    public void SetModel()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        transform.GetChild(towerLevel).gameObject.SetActive(true);
    }
    #endregion
    #region Ÿ�� ���� ���� �Լ�
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
    #region ��� ����
    void Shot()
    {
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
        //��ž ȸ�� ����
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;        
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0);


        //��� ����
        if(shotCountdown <= 0f)
        {
            Shot();
            shotCountdown = 1f / speed;
        }
        shotCountdown -= Time.deltaTime;


        if (Input.GetKeyDown(KeyCode.U))
        {
            SetState();
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    #endregion
}

