using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class money : MonoBehaviour
{
    public float wallet = 0;
    [HideInInspector]
    //public float gunUsing;
    public int TotalEnemyDieRocket = 0, TotalEnemyDieSuicide = 0, TotalEnemyDieBurst = 0, TotalEnemyDieShotGun = 0, TotalEnemyDie, TotalDamageDealt, totalDamageTaken;
    public void CollectMoney(float money)
    {
        //   money *= gunUsing;
        wallet += money;
    }
    public void AddEnemyDie(int OneRoc2Sui3Bur4Shot, int HowManyEnemyDie)
    {
        if (OneRoc2Sui3Bur4Shot == 1) { TotalEnemyDieRocket += HowManyEnemyDie; }
        if (OneRoc2Sui3Bur4Shot == 2) { TotalEnemyDieSuicide += HowManyEnemyDie; }
        if (OneRoc2Sui3Bur4Shot == 3) { TotalEnemyDieBurst += HowManyEnemyDie; }
        if (OneRoc2Sui3Bur4Shot == 4) { TotalEnemyDieShotGun += HowManyEnemyDie; }
        TotalEnemyDie = TotalEnemyDieBurst + TotalEnemyDieRocket + TotalEnemyDieSuicide + TotalEnemyDieShotGun;

    }

}
