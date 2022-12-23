using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHandler : MonoBehaviour
{
    #region 변수

    LivingParticleController living;
    [SerializeField]
    Transform livingFoot;

    public int level;
    public float lifeTime;
    public GameObject enemyDiePrefab;

    private Transform target;
    private int pointIndex = 0;

    [Header("Unit")]
    public Image healthBar;

    // 파라미터
    EnemyParam enemy;
    private int attack;
    private float health;
    private float maxHelth;
    private float startSpeed;
    private float speed;
    private float guard;
    private int value;
    [SerializeField]
    EnemyScriptable enemyContainer;
    #endregion
    #region 함수
    private void Start()
    {
        target = WayPointHandler.points[0];

        level = GameManager.gameLevel;

        enemy = enemyContainer.GetEnemyList(level % 10);
        attack = enemy.attack;
        health = enemy.health;
        maxHelth = enemy.health;
        speed = enemy.speed;
        startSpeed = enemy.speed;
        guard = enemy.armor;

        if(level >= 10)
        {
            health *= 2;
            maxHelth *= 2;
            speed *= 1.2f;
            startSpeed *= 1.2f;
            guard *= 2;
        }

        value = 5;
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

        speed = startSpeed;        
    }

    void GetNextWayPoint()
    {
        if(pointIndex >= WayPointHandler.points.Length)
        {
            EndPath();

            return;
        }
        target = WayPointHandler.points[pointIndex];
        pointIndex++;
    }

    void EndPath()
    {
        SoundManager.instance.SoundByNum(4);
        GameManager.instance.AlterPopup(4);
        PlayerController.life -= attack;

        if(PlayerController.life < 0)
        {
            PlayerController.life = 0; 
        }
        Destroy(gameObject);
    }

    public void TakeDamage(float amount)
    {
        float damage = amount - guard;
        if (damage <= 0) damage = 0.01f;

        health -= damage;
        healthBar.fillAmount = health / maxHelth;

        if(health <= 0)
        {
            Die();
        }
    }

    public void Slow(float pct)
    {
        speed = startSpeed * (1 - pct);

    }

    void Die()
    {
        GameObject effect = Instantiate(enemyDiePrefab, transform.position, Quaternion.identity);
        Destroy(effect, 2f);

        PlayerController.money += value;
        Destroy(gameObject);        
    }
    #endregion
}
