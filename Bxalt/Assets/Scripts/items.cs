using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class items : MonoBehaviour
{
    public float HowMany;
    public bool isHealItems, isReloadItem, isInCreaseMaxHealth, Instant, plusHowManyOrMultipliHowMany, isNade, isIncreaseSpeedItem, isKhauTrang = false, isShield = false;
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
                if (isShield)
                {
                    shield();
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
            if (isIncreaseSpeedItem)
            {
                if (Input.GetButtonDown("Use" + Index.ToString()))
                {
                    StartCoroutine(IncreaseSpeed(5f));
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
            if (isKhauTrang)
            {
                GameObject.FindGameObjectWithTag("MaskForPlayer").GetComponent<SpriteRenderer>().enabled = true;
                PlayerHealth.gameObject.GetComponent<PlayerTopDownMovement>().sync();
            }
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
    IEnumerator IncreaseSpeed(float LastHowLong)
    {
        GameObject.FindObjectOfType<PlayerTopDownMovement>().BoostSpeed = 3;
        GameObject.FindObjectOfType<itemsUI>().DeleteImage();
        yield return new WaitForSeconds(LastHowLong);
        GameObject.FindObjectOfType<PlayerTopDownMovement>().BoostSpeed = 1;
        Destroy(this.gameObject);
    }
    void shield()
    {
        if (GameObject.FindGameObjectWithTag("ShieldForPlayer").GetComponent<SpriteRenderer>() != null)
        {
            GameObject.FindGameObjectWithTag("ShieldForPlayer").GetComponent<SpriteRenderer>().enabled = true;
            PlayerHealth.gameObject.GetComponent<PlayerTopDownMovement>().sync();
        }
        if (GameObject.FindGameObjectWithTag("shieldIconForPlayer").GetComponent<Image>() != null)
        {
            GameObject.FindGameObjectWithTag("shieldIconForPlayer").GetComponent<Image>().enabled = true;
        }
    }
}
