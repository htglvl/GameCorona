using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 100;
    public int OneRoc2Sui3Bur4Shot;
    public float health;
    public bool CalculateLoot = false, DestroyOnTime = false;
    public bool IsPlayer = false, DeathTick = false, isBullet = false;
    float quarantineTime, quarantineMultiplier;
    public TextMeshProUGUI DanhSo;
    bool inQuarantine = false;


    // Start is called before the first frame update
    void Start()
    {
        if (IsPlayer)
        {
            GameObject.FindGameObjectWithTag("DoorQuarantineForPlayer").GetComponent<BoxCollider2D>().enabled = false;
            quarantineMultiplier = GameObject.FindObjectOfType<DaynightCycle>().timeMultiplyer;
        }
        health = maxHealth;
        if (DestroyOnTime)
        {
            Destroy(gameObject, 10f);
        }
    }
    private void Update()
    {
        if (IsPlayer && inQuarantine == true)
        {
            quarantineTime -= Time.deltaTime * quarantineMultiplier;
            DanhSo.text = (Mathf.FloorToInt(quarantineTime / 24f) + 1).ToString();
        }
        if (quarantineTime <= 0)
        {
            quarantineTime = GameObject.FindObjectOfType<randomDestinationAI>().quarantineTime;
            health = maxHealth;
            inQuarantine = false;
            GameObject.FindGameObjectWithTag("DoorQuarantineForPlayer").GetComponent<BoxCollider2D>().enabled = false;
            GameObject.FindGameObjectWithTag("MaskForPlayer").GetComponent<SpriteRenderer>().enabled = true;
        }
        if (IsPlayer)
        {
            if (quarantineTime <= 2 || DanhSo.text == "0")
            {
                DanhSo.text = "";
            }
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
            if (inQuarantine == false)
            {
                GameObject.FindGameObjectWithTag("MaskForPlayer").GetComponent<SpriteRenderer>().enabled = false;
                inQuarantine = true;
                transform.position = GameObject.FindGameObjectWithTag("QuarantineSpotForPlayer").transform.position;
                GameObject.FindGameObjectWithTag("DoorQuarantineForPlayer").GetComponent<BoxCollider2D>().enabled = true;
                quarantineTime = GameObject.FindObjectOfType<randomDestinationAI>().quarantineTime;

            }




            //FindObjectOfType<GameManager>().EndGame();
            //GetComponent<PlayerTopDownMovement>().enabled = false;
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
            if (!IsPlayer)
            {
                Destroy(gameObject);
            }
        }
    }
}
