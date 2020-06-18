using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class waypointNavigator : MonoBehaviour
{

    IAstarAI ai;
    public WayPoint currentwayPoint;
    int direction;
    // Start is called before the first frame update
    private void Awake()
    {
        ai = GetComponent<IAstarAI>();
        ai.maxSpeed = ai.maxSpeed + Random.Range(-2f, 1f);
    }
    void Start()
    {
        direction = Mathf.RoundToInt(Random.Range(0f, 1f));
        ai.destination = currentwayPoint.GetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (ai.reachedDestination)
        {
            bool shouldBranch = false;

            if (currentwayPoint.branches != null && currentwayPoint.branches.Count > 0)
            {
                shouldBranch = Random.Range(0f, 1f) <= currentwayPoint.branchRatio ? true : false;
            }
            if (shouldBranch)
            {
                currentwayPoint = currentwayPoint.branches[Random.Range(0, currentwayPoint.branches.Count - 1)];
            }
            else
            {
                if (direction == 0)
                {
                    if (currentwayPoint.nextWaypoint != null)
                    {
                        currentwayPoint = currentwayPoint.nextWaypoint;
                    }
                    else
                    {
                        currentwayPoint = currentwayPoint.previousWaypoint;
                        direction = 1;
                    }

                }
                else if (direction == 1)
                {
                    if (currentwayPoint.previousWaypoint != null)
                    {
                        currentwayPoint = currentwayPoint.previousWaypoint;
                    }
                    else
                    {
                        currentwayPoint = currentwayPoint.nextWaypoint;
                        direction = 0;
                    }
                }
                ai.destination = currentwayPoint.GetPosition();
            }


        }
    }
}
