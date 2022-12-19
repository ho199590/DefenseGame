using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TowerParam
{
    public float attack;
    public float speed;
    public float range;
    public bool skill;
    public GameObject bullet;    
}
[System.Serializable]
public class TowerClass
{
    public string name;
    public string code;
    public List<TowerParam> level;
}

[CreateAssetMenu(menuName = "ScriptableObject/Tower", fileName = "TowerContainer")]
public class TowerScriptable : ScriptableObject
{
    [SerializeField]
    private List<TowerClass> towerList = new List<TowerClass>();


    public TowerClass GetTowerState(int code)
    {
        return towerList[code];
    }
}
