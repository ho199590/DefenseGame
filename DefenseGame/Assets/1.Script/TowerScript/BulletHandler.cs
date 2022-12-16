using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{
    #region 변수
    private Transform target;
    public float speed = 70;
    public GameObject bulletParticle;
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
    }

    public void HitTarget()
    {
        GameObject effect =  Instantiate(bulletParticle, transform.position, transform.rotation);
        effect.transform.localScale = Vector3.one * 2;

        Destroy(effect, 2f);
        Destroy(this.gameObject);
    }
    #endregion
}
