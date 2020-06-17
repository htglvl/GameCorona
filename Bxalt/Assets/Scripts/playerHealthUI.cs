using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class playerHealthUI : MonoBehaviour
{
    private Enemy playerHealth;
    private money Money;
    public Image health;
    public TextMeshProUGUI HealthText, Wallet;
    private float currentHealth, maxHealth;
    public Color FullHealthColor, ZeroHealthColor;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Enemy>();
        Money = playerHealth.gameObject.GetComponent<money>();
    }

    // Update is called once per frame
    void Update()
    {
        maxHealth = playerHealth.maxHealth;
        currentHealth = playerHealth.health;
        health.fillAmount = currentHealth / maxHealth;
        HealthText.text = Mathf.FloorToInt(currentHealth).ToString() + "/" + Mathf.FloorToInt(maxHealth).ToString();
        health.color = Color.Lerp(ZeroHealthColor, FullHealthColor, health.fillAmount);
        Wallet.text = "Money: " + Money.wallet.ToString();
    }
}
