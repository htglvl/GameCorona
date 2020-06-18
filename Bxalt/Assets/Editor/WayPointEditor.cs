using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad()]
public class WayPointEditor
{
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
    public static void OnDrawSceneGizmo(WayPoint wayPoint, GizmoType gizmoType)
    {
        if ((gizmoType & GizmoType.Selected) != 0)
        {
            Gizmos.color = Color.yellow;
        }
        else
        {
            Gizmos.color = Color.yellow * 0.5f;
        }

        Gizmos.DrawSphere(wayPoint.transform.position, .1f);
        Gizmos.DrawLine(wayPoint.transform.position + (wayPoint.transform.right * wayPoint.width / 2f), wayPoint.transform.position - (wayPoint.transform.right * wayPoint.width / 2f));

        if (wayPoint.previousWaypoint != null)
        {
            Gizmos.color = Color.red;
            Vector3 offset = wayPoint.transform.right * wayPoint.width / 2;
            Vector3 offsetTo = wayPoint.previousWaypoint.transform.right * wayPoint.previousWaypoint.width / 2;
            Gizmos.DrawLine(wayPoint.transform.position + offset, wayPoint.previousWaypoint.transform.position + offsetTo);
        }

        if (wayPoint.nextWaypoint != null)
        {
            Gizmos.color = Color.green;
            Vector3 offset = wayPoint.transform.right * -wayPoint.width / 2;
            Vector3 offsetTo = wayPoint.nextWaypoint.transform.right * -wayPoint.nextWaypoint.width / 2;
            Gizmos.DrawLine(wayPoint.transform.position + offset, wayPoint.nextWaypoint.transform.position + offsetTo);
        }

        if (wayPoint.branches != null)
        {
            foreach (WayPoint branch in wayPoint.branches)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(wayPoint.transform.position, branch.transform.position);
            }
        }
    }
}
