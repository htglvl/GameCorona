using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathScreen : MonoBehaviour
{
    public TextMeshProUGUI totalDamageDealt, totalDamageTaken, kills;
    public void GetDeathStats()
    {
        money PlayerStat = FindObjectOfType<money>();
        totalDamageDealt.text = "Total Damage Dealt: " + PlayerStat.TotalDamageDealt.ToString();
        totalDamageTaken.text = "Total Damage Taken: " + PlayerStat.totalDamageTaken.ToString();
        kills.text = "Kills: " + PlayerStat.TotalEnemyDie.ToString();
        // rocketKill.text = PlayerStat.TotalEnemyDieRocket.ToString();
        // suicidekill.text = PlayerStat.TotalEnemyDieSuicide.ToString();
        // burstKill.text = PlayerStat.TotalEnemyDieBurst.ToString();
        // shotKill.text = PlayerStat.TotalEnemyDieShotGun.ToString();
    }
}
