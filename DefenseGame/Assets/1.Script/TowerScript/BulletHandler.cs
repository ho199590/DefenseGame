using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{
    #region 변수
    private Transform target;
    public float speed = 70;
    public float explosionRadius = 0;
    public GameObject bulletParticle;
    public GameObject explodeParticle;
    #endregion
    #region 함수
    public void SetTarget(Transform _target) { target = _target; }
    private void Update()
    {
        if(target == null){Destroy(this.gameObject);return; }
        
        Vector3 dir = target.position - transform.position;
        float distanceThis = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThis)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThis, Space.World);
        transform.LookAt(target);
    }

    public void HitTarget()
    {
        GameObject effect =  Instantiate(bulletParticle, transform.position, transform.rotation);
        effect.transform.localScale = Vector3.one * 2;

        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }

        Destroy(effect, 2f);
        Destroy(this.gameObject);
    }

    public void Explode()
    {
        Collider[] colliders =  Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider collider in colliders)
        {
            if(collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }

        GameObject effect = Instantiate(explodeParticle, transform.position, transform.rotation);
        Destroy(effect, 2f);
    }

    void Damage(Transform enemy)
    {


        //Destroy(enemy);
    }
    #endregion
    #region 디버그 용
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
    #endregion
}
