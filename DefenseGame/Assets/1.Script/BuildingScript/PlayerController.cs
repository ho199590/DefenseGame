using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillSet
{
    public string name;
    public int cost;
    public float damage;
    public Vector3 offset;
    public GameObject effect;
}

public class PlayerController : MonoBehaviour
{
    #region 변수
    public static int money;
    public static int life;
    public int startMony = 100;
    public int startLife = 10;

    #region 스킬

    Ray ray;
    RaycastHit hit;
    [Header("스킬 세팅")]
    [SerializeField]
    LayerMask mask;
    [SerializeField]
    SkillSet skill;
    #endregion
    #endregion

    #region 함수
    private void Start()
    {
        money = startMony;
        life = startLife;

        hit = new RaycastHit();
    }

    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if(BuilderController.instance.playerOP == PlayerOperate.Destroy)
            {
                Drop();
            }
        }
    }
    public void Drop()
    {
        if(money < skill.cost)
        {
            GameManager.instance.AlterPopup(0);
            return;
        }

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        {
            money -= skill.cost;
            var active = Instantiate(skill.effect, hit.point + skill.offset, Quaternion.identity);
            active.GetComponent<BombScript>().SetParam(skill);
        }
    }


    #endregion
}
