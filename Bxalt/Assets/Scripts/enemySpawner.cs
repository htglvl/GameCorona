using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    private Transform player;
    public int HowmanyEnemyShouldExist;
    private int currentEnemy = 0;
    private int howManyEnemyShouldWeSpawn;
    public float SpawnDelay = 1f;
    public GameObject[] Enemy;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        currentEnemy = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if ((currentEnemy < HowmanyEnemyShouldExist) && player != null)
        {
            howManyEnemyShouldWeSpawn = Random.Range(HowmanyEnemyShouldExist - currentEnemy, HowmanyEnemyShouldExist);
            for (int i = 0; i < howManyEnemyShouldWeSpawn; i++)
            {
                Instantiate(Enemy[Random.Range(0, Enemy.Length)], player.position + new Vector3(Random.Range(-30f, 30f), Random.Range(-30f, 30f)), Quaternion.identity);
            }
        }
    }
}
