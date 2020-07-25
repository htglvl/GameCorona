using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

public class randomDestinationAI : MonoBehaviour
{
    public Image QuarantineImage, StopImage;
    IAstarAI ai;
    public Vector2 TopLeft, BottomRight, pos, topleftQua, BottomRightQua;
    CircleCollider2D cc2D;
    public bool stop, quarantine;
    public float quarantineTime, stopTime;
    private float Pri_quarantineTime, Pri_stopTime;
    private bool haveJustBeenQuaed = false, denKhuCachLy, haveJustBeenStopped = false;
    // Start is called before the first frame update
    void Start()
    {
        Pri_stopTime = stopTime;
        Pri_quarantineTime = quarantineTime;
        ai = GetComponent<IAstarAI>();
        cc2D = GetComponent<CircleCollider2D>();
        PickRandomSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (ai.reachedDestination && stop == false && quarantine == false)
        {
            PickRandomSpawn();
        }
        if (stop)
        {
            if (haveJustBeenStopped == false)
            {
                haveJustBeenStopped = true;
                Pri_stopTime = stopTime;
            }
            if (haveJustBeenStopped == true)
            {
                Pri_stopTime -= Time.deltaTime;
            }
            if (Pri_stopTime <= 0)
            {
                stop = false;
                haveJustBeenStopped = false;
                Pri_stopTime = stopTime;
            }
            ai.destination = transform.position;
        }
        if (quarantine)
        {
            if (haveJustBeenQuaed == false)
            {
                PickRandomQua();
                haveJustBeenQuaed = true;
                Pri_quarantineTime = quarantineTime;
            }
            if (ai.reachedDestination)
            {
                PickRandomQua();
                denKhuCachLy = true;
                GetComponent<AIBrain>().DenkhuCachLy = true;
            }
            if (denKhuCachLy)
            {
                Pri_quarantineTime -= Time.deltaTime;
            }
        }
        if (Pri_quarantineTime <= 0)
        {
            Pri_quarantineTime = quarantineTime;
            quarantine = false;
            haveJustBeenQuaed = false;
            denKhuCachLy = false;
            GetComponent<AIBrain>().DenkhuCachLy = false;
        }
        if (quarantine)
        {
            QuarantineImage.enabled = true;
        }
        else
        {
            QuarantineImage.enabled = false;
        }

        if (stop)
        {
            StopImage.enabled = true;
        }
        else
        {
            StopImage.enabled = false;
        }
    }
    void PickRandomQua()
    {
        do
        {
            pos.x = Random.Range(topleftQua.x, BottomRightQua.x);
            pos.y = Random.Range(BottomRightQua.y, topleftQua.y);
        } while (Physics2D.OverlapCircle(pos, cc2D.radius) != null);
        ai.destination = pos;
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
        Gizmos.DrawSphere(topleftQua, 1f);
        Gizmos.DrawSphere(BottomRightQua, 1f);
    }
}
