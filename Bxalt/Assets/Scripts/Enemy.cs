using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 100;
    public int OneRoc2Sui3Bur4Shot;
    public float health;
    public bool CalculateLoot = false, DestroyOnTime = false;
    public bool IsPlayer = false, DeathTick = false, isBullet = false;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        if (DestroyOnTime)
        {
            Destroy(gameObject, 10f);
        }
    }

    // Update is called once per frame
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (IsPlayer)
        {
            gameObject.GetComponent<money>().totalDamageTaken += damage;
        }
        if (health <= 0)
        {
            health = 0;
            Die();
        }

    }
    void Die()
    {
        if (CalculateLoot)
        {
            GetComponent<DropItem>().caclculateLoot();
        }
        if (IsPlayer)
        {
            FindObjectOfType<GameManager>().EndGame();
            GetComponent<PlayerTopDownMovement>().enabled = false;
        }
        if (DeathTick)
        {
            FindObjectOfType<money>().AddEnemyDie(OneRoc2Sui3Bur4Shot, 1);
        }
        if (isBullet)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
