using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnController : MonoBehaviour
{
    #region 변수
    [Header("스폰 지점 / 범위")]
    public Vector3 center;
    public Vector3 size;

    // 적 프리팹
    public Transform enemyPrefab;
    // 첫 웨이브 생성까지 카운트 다운
    private float countdown = 5.5f;
    [HideInInspector]
    public int waveLevel = 0;
    [SerializeField]
    private int waveCount;

    public float timeBetweenWaves = 5.5f;

    [SerializeField]
     Text waveCountdownText;

    private bool spawning = false;

    [SerializeField]
    EnemyScriptable enemy;
    #endregion
    private void Start()
    {
        waveCountdownText.text = "다음 웨이브 : " + Mathf.Floor(countdown).ToString();
    }

    #region 함수
    private void Update()
    {
        if (countdown <= 0)
        {
            StartCoroutine(SpawnWave());    
            countdown = timeBetweenWaves;
        }

        if (!spawning)
        {
            countdown -= Time.deltaTime;
            waveCountdownText.text = "다음 웨이브 : " + Mathf.Floor(countdown).ToString();
        }
    }


    IEnumerator SpawnWave()
    {
        Debug.Log("Wave comming");


        int test = waveLevel % 2;        
        waveCount = enemy.GetEnemyList(test).count;

        spawning = true;
        waveCountdownText.text = "다음 웨이브 : 0";
        for (int i = 0; i < waveCount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }

        spawning = false;
        waveLevel++;
        yield break;
    }

    void SpawnEnemy()
    {
        float x = Random.Range(-(size.x / 2), (size.x / 2));
        float z = Random.Range(-(size.y / 2), (size.y / 2));
        Vector3 SpawnPoint = new Vector3(center.x, 1, center.z);
        var enemy = Instantiate(enemyPrefab, SpawnPoint, Quaternion.identity);
        enemy.tag = "Enemy";
    }
    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }
}
