 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    private Enemy enemyTarget;

    [Header("General Attributes")]
    public float range = 15f;

    [Header("Bullet Attributes")] 
    public GameObject bulletPrefab;    
    public float fireRate = 1f;
    private float fireCount = 0f;

    [Header("Laser Attributes")]
    public bool lasering = false;

    public int damagePerTime = 30;
    public float slowPts = .33f;

    public LineRenderer lineRenderer_;
    public ParticleSystem laseringParticles;
    public Light laseringLight;
    

    [Header("Setup")]

    public string enemyTag = "Enemy";

    public Transform partToRotate;

    public float turningSpeed = 10f;


    public Transform firePoint;

    void Start()
    {
        InvokeRepeating("updateTarget", 0f, 0.5f);
    }

    void updateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies) 
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }

            if(nearestEnemy != null && shortestDistance <= range)
            {
                target = nearestEnemy.transform;
                enemyTarget = nearestEnemy.GetComponent<Enemy>();
            }
            else
            {
                target = null;
            }
        }
    }

    void Update()
    {
        if (target == null)
        {
            if (lasering)
            {
                if(lineRenderer_.enabled)
                {
                    lineRenderer_.enabled = false;
                    laseringParticles.Stop();
                    laseringLight.enabled = false;
                }
            }          
            return;
        }           

        targetTheTarget();

        if (lasering)
        {
            laser();
        }
        else
        {
            if (fireCount <= 0f)
            {
                Shoot();
                fireCount = 1f / fireRate;
            }

            fireCount -= Time.deltaTime;
        }
    }

    void laser()
    {
        enemyTarget.takingDamage(damagePerTime * Time.deltaTime);
        enemyTarget.slowByLaser(slowPts);
        
        if (!lineRenderer_.enabled)
        {
            lineRenderer_.enabled = true;
            laseringParticles.Play();
            laseringLight.enabled = true;
        }
        
        lineRenderer_.SetPosition(0, firePoint.position);
        lineRenderer_.SetPosition(1, target.position);

        Vector3 direction = firePoint.position - target.position;

        laseringParticles.transform.position = target.position + direction.normalized;
        
        laseringParticles.transform.rotation = Quaternion.LookRotation(direction);
    }

    void targetTheTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turningSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void OnDrawGizmosSelected()  
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void Shoot()
    {
        //Debug.Log("SHOOT");
        GameObject bulletGameObject = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGameObject.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Aim(target);
        }
    }
}
