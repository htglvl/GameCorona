using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawner : MonoBehaviour
{
    public float HowManyAIInScenePatrol, RandomAi;
    public GameObject EnemyAI, EnemyAIRandom;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());

    }

    IEnumerator Spawn()
    {
        for (int i = 0; i < HowManyAIInScenePatrol; i++)
        {
            GameObject obj = Instantiate(EnemyAI);
            Transform child = transform.GetChild(Random.Range(0, transform.childCount - 1));
            obj.GetComponent<waypointNavigator>().currentwayPoint = child.GetComponent<WayPoint>();
            obj.transform.position = child.position;
            yield return new WaitForEndOfFrame();
        }
        for (int i = 0; i < RandomAi; i++)
        {
            GameObject obj = Instantiate(EnemyAIRandom);
        }
    }
}
