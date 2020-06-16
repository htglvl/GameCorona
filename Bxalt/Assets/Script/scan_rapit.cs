using UnityEngine;
using Pathfinding;

public class scan_rapit : MonoBehaviour
{
    public float rerate;
    private void Start()
    {
        InvokeRepeating("RS", 1f, rerate);
    }
    private void RS()
    {
        AstarPath.active.Scan();
    }
}