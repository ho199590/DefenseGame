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
    public Transform[] enemyPrefab;
    [SerializeField]
    Transform enemyBasket;
    // 첫 웨이브 생성까지 카운트 다운
    private float countdown = 5.5f;    
    [SerializeField]
    private int waveCount;

    public float timeBetweenWaves = 5.5f;

    [SerializeField]
     Text waveCountdownText;

    private bool spawning = false;

    [SerializeField]
    EnemyScriptable enemy;

    [SerializeField]
    List<SoundParam> sounds = new List<SoundParam>();
    [SerializeField]
    List<Material> mats = new List<Material>();
    [SerializeField]
    WayRanderController ways;
    #endregion
    private void Start()
    {
        waveCountdownText.text = GameManager.gameLevel.ToString() + " 웨이브 : " + Mathf.Floor(countdown).ToString();
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
            waveCountdownText.text = GameManager.gameLevel.ToString() + " 웨이브 : " + Mathf.Floor(countdown).ToString();
        }
    }


    IEnumerator SpawnWave()
    {
        Debug.Log("Wave comming");


        int test = GameManager.gameLevel % 10;        
        waveCount = enemy.GetEnemyList(test).count;

        if(test == 9)
        {   
            GameManager.instance.AlterPopup(5);
        }

        spawning = true;
        waveCountdownText.text = GameManager.gameLevel.ToString() + " 웨이브 : 0";
        for (int i = 0; i < waveCount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }

        spawning = false;
        GameManager.gameLevel++;
        yield break;
    }

    void SpawnEnemy()
    {
        float x = Random.Range(-(size.x / 2), (size.x / 2));
        float z = Random.Range(-(size.y / 2), (size.y / 2));
        Vector3 SpawnPoint = new Vector3(center.x, 1, center.z);
        SoundManager.instance.SoundOnShot(sounds[0].clip);

        if (GameManager.gameLevel % 10 == 9)
        {   
            SoundManager.instance.SoundOnShot(sounds[1].clip);
            var enemy = Instantiate(enemyPrefab[1], SpawnPoint, Quaternion.identity, enemyBasket);
            enemy.GetComponent<Renderer>().material = mats[(int)GameManager.gameLevel/10];

            ways.SetNextMaterial();
            SoundManager.instance.SetNextBgm();

            GameManager.instance.grade++;
        }
        else
        {   
            var enemy = Instantiate(enemyPrefab[0], SpawnPoint, Quaternion.identity, enemyBasket);
            enemy.GetComponent<Renderer>().material = mats[(int)GameManager.gameLevel / 10];
        }
        


        //enemy.tag = "Enemy";
    }
    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }
}
