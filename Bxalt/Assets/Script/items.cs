﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class items : MonoBehaviour
{
    public float HowMany, howLongOnlyNotInstant;
    public bool isHealItems, isReloadItem, isInCreaseMaxHealth, Instant, plusHowManyOrMultipliHowMany, isNade;
    public int Index;
    public Enemy PlayerHealth;
    public string mieuTa;
    public GameObject nade;
    private void OnEnable()
    {
        PlayerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Enemy>();
        if (isNade)
        {
            GameObject.FindObjectOfType<itemsUI>().TextShowHowMany(HowMany.ToString());
        }
        else
        {
            GameObject.FindObjectOfType<itemsUI>().TextShowHowMany("");
        }
    }
    private void Update()
    {
        if (Instant)
        {
            if (Input.GetButtonDown("Use" + Index.ToString()))
            {
                if (isHealItems)
                {
                    Heal(HowMany);
                }
                if (isInCreaseMaxHealth)
                {
                    IncreaseMaxHealth(HowMany);
                }
                if (isReloadItem)
                {
                    InstanceReload();
                }
                GameObject.FindObjectOfType<itemsUI>().DeleteImage();
                Destroy(this.gameObject);
            }
        }
        else
        {
            if (isNade)
            {
                if (Input.GetButtonDown("Use" + Index.ToString()))
                {
                    if (HowMany > 0)
                    {
                        Instantiate(nade, transform.position, transform.rotation);
                        HowMany--;
                        GameObject.FindObjectOfType<itemsUI>().TextShowHowMany(HowMany.ToString());
                    }
                    else
                    {
                        Destroy(this.gameObject);
                    }
                }
            }
        }
        if (PlayerHealth.health > PlayerHealth.maxHealth)
        {
            PlayerHealth.health = PlayerHealth.maxHealth;
        }
    }

    public void Heal(float baonhieu)
    {
        if (plusHowManyOrMultipliHowMany)
        {

            PlayerHealth.health += baonhieu;
        }
        else
        {
            PlayerHealth.health *= baonhieu;
        }
    }
    public void IncreaseMaxHealth(float baonhieu)
    {
        if (plusHowManyOrMultipliHowMany)
        {
            PlayerHealth.maxHealth += baonhieu;
        }
        else
        {
            PlayerHealth.maxHealth *= baonhieu;
        }
    }
    public void InstanceReload()
    {
        GameObject.FindObjectOfType<PlayerTopDownShooting>().currentammo = GameObject.FindObjectOfType<PlayerTopDownShooting>().maxammo;
    }
}
