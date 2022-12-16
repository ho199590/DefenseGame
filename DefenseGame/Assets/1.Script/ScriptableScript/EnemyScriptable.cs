using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyParam
{
    public int count;
    public int attack;
    public float health;
    public float armor;
    public float speed;    
    public bool skill;    
}
[CreateAssetMenu(menuName = "ScriptableObject/Enemy", fileName = "EnemyContainer")]
public class EnemyScriptable : ScriptableObject
{
    [SerializeField]
    List<EnemyParam> enemyList = new List<EnemyParam>();


    public EnemyParam GetEnemyList(int num)
    {
        return enemyList[num];
    }
}
