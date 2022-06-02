using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 70f;

    public int bulletDamage = 50;

    public float explosionRange = 0f;

    public GameObject particleEffect;

    private Transform target;

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceAtFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceAtFrame)
        {
            hitTheTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceAtFrame, Space.World);

        transform.LookAt(target);
    }

    public void Aim(Transform target_)
    {
        target = target_;
    }

    void hitTheTarget()
    {
        //Debug.Log("HIT THE TARGET");

        GameObject gameEffect = (GameObject)Instantiate(particleEffect, transform.position, transform.rotation);
        
        Destroy(gameEffect, 4f);

        if (explosionRange >= 0f)
        {
            explode();
        }
        else
        {
            damage(target);
        }

        Destroy(gameObject);
    }

    void explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRange);

        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                damage(collider.transform);
            }
        }
    }

    void damage(Transform enemy)
    {
        Enemy en = enemy.GetComponent<Enemy>();

        if(en != null)
        {
            en.takingDamage(bulletDamage);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
