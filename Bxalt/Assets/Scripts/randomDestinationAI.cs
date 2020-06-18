using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class randomDestinationAI : MonoBehaviour
{
    IAstarAI ai;
    public Vector2 TopLeft, BottomRight, pos;
    CircleCollider2D cc2D;
    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponent<IAstarAI>();
        cc2D = GetComponent<CircleCollider2D>();
        PickRandomSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (ai.reachedDestination)
        {
            PickRandomSpawn();
        }
    }
    void PickRandomSpawn()
    {
        do
        {
            pos.x = Random.Range(TopLeft.x, BottomRight.x);
            pos.y = Random.Range(BottomRight.y, TopLeft.y);
        } while (Physics2D.OverlapCircle(pos, cc2D.radius) != null);
        ai.destination = pos;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(TopLeft, 1f);
        Gizmos.DrawSphere(BottomRight, 1f);
    }
}
