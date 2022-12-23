using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    #region 변수
    [SerializeField]
    LayerMask targetMask;

    [SerializeField]
    private float explosionRadius;
    private float damage;

    [SerializeField]
    GameObject bombEffect;
    [SerializeField]
    AudioClip bombSound;
    #endregion

    #region 함수

    public void SetParam(SkillSet skill)
    {
        damage = skill.damage;
    }

    private void OnTriggerEnter(Collider other)
    {   
        Explode();
    }

    public void Explode()
    {
        GetComponent<Collider>().enabled = false;
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {   
                print("TEST");
                Damage(collider.transform);
            }
        }

        SoundManager.instance.SoundOnShot(bombSound);
        GameObject effect = Instantiate(bombEffect, transform.position, transform.rotation);
        //Destroy(effect, 2f);
        Destroy(gameObject, 2f);
        
    }

    void Damage(Transform enemy)
    {
        if (enemy != null)
        {
            enemy.GetComponent<EnemyHandler>().TakeDamage(damage);            
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
    #endregion
}
