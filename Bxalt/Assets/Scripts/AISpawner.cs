using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawner : MonoBehaviour
{
    public float HowManyAIInScenePatrol, RandomAi;
    public GameObject EnemyAI, EnemyAIRandom;
    public Vector2 TopLeftCorner, BottomRightCorner;
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
            Vector2 pos;
            do
            {
                pos = new Vector2(Random.Range(TopLeftCorner.x, BottomRightCorner.x), Random.Range(BottomRightCorner.y, TopLeftCorner.y));
            } while (Physics2D.OverlapCircle(pos, 1) == null);
            GameObject obj = Instantiate(EnemyAIRandom, pos, transform.rotation);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(TopLeftCorner, 1f);
        Gizmos.DrawWireSphere(BottomRightCorner, 1f);
    }
}
