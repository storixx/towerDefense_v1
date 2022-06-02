using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public float speed;

    public float startSpeed = 10f;

    public float health = 100;

    public int moneyForKill = 50;

    public GameObject deathParticle;

    private void Start()
    {
        speed = startSpeed;
    }

    public void takingDamage(float amountOfDamage)
    {
        health -= amountOfDamage;

        if(health <= 0)
        {
            death();
        }
    }

    public void slowByLaser(float pts)
    {
        speed = startSpeed * (1f - pts);
    }

    void death()
    {
        GameObject particle = (GameObject)Instantiate(deathParticle, transform.position, Quaternion.identity);
        Destroy(particle, 5f);
        
        PlayerStats.playerMoney += moneyForKill;
        Destroy(gameObject);
    }
}