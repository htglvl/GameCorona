
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemySpawnWave : MonoBehaviour
{
    public GameObject[] Enemy;
    public Slider timeEnemyReachPlayer;
    public TextMeshProUGUI SoWave;
    private Transform player;
    public int[] waweEnemy, waitTimeBetweenWave;
    int currentWave = 0, currentEnemy;
    float currentTime = 0;
    public int SoEnemyTang, soAutoWave = 0;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        currentEnemy = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (currentEnemy <= 0)
        {
            currentTime += Time.deltaTime;
            if (currentTime > waitTimeBetweenWave[currentWave])
            {

                SpawnWave(waweEnemy[currentWave]);
                if (currentWave == waitTimeBetweenWave.Length - 1)
                {
                    waweEnemy[currentWave] += SoEnemyTang;
                    soAutoWave++;
                }
                else
                {
                    currentWave++;
                }
            }
        }
        else
        {
            currentTime = 0;
        }
        SoWave.text = "Wave " + (currentWave + soAutoWave).ToString();
        timeEnemyReachPlayer.maxValue = waitTimeBetweenWave[currentWave];
        timeEnemyReachPlayer.value = currentTime;
    }
    void SpawnWave(int SoLuongEnemy)
    {
        for (int i = 0; i < SoLuongEnemy; i++)
        {
            float randomX = Random.Range(1, 3);
            float randomY = Random.Range(1, 3);
            if (randomX == 1)
            {
                randomX = Random.Range(-30f, -10f);
            }
            else
            {
                randomX = Random.Range(10f, 30f);
            }
            if (randomY == 1)
            {
                randomY = Random.Range(-30f, -10f);
            }
            else
            {
                randomY = Random.Range(10f, 30f);
            }
            Instantiate(
                Enemy[Random.Range(0, Enemy.Length)], player.position + new Vector3(randomX, randomY), Quaternion.identity);
        }
    }
}
