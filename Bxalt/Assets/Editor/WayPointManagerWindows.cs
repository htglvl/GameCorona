using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WayPointManagerWindows : EditorWindow
{
    [MenuItem("Tools/WayPoint Editor")]
    public static void Open()
    {
        GetWindow<WayPointManagerWindows>();
    }
    public Transform waypointRoot;
    private void OnGUI()
    {
        SerializedObject obj = new SerializedObject(this);
        EditorGUILayout.PropertyField(obj.FindProperty("waypointRoot"));

        if (waypointRoot == null)
        {
            EditorGUILayout.HelpBox("Root Transform must be selected. Assign a root transform now", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.BeginVertical("box");
            DrawButton();
            EditorGUILayout.EndVertical();
        }

        obj.ApplyModifiedProperties();
    }
    void DrawButton()
    {
        if (GUILayout.Button("Creat Waypoint"))
        {
            CreatWayPoint();
        }
        if (Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<WayPoint>())
        {
            if (GUILayout.Button("Creat Branches"))
            {
                CreatBranch();
            }
            if (GUILayout.Button("Creat WayPoint Before"))
            {
                CreatWayPointBefore();
            }
            if (GUILayout.Button("Creat WayPoint After"))
            {
                CreatWayPointAfter();
            }
            if (GUILayout.Button("Remove Waypoint"))
            {
                RemoveWayPoint();
            }
        }
    }
    public void CreatWayPoint()
    {
        GameObject wayPointObj = new GameObject("Waypoint " + waypointRoot.childCount, typeof(WayPoint));
        wayPointObj.transform.SetParent(waypointRoot, false);
        WayPoint wayPoint = wayPointObj.GetComponent<WayPoint>();
        if (waypointRoot.childCount > 1)
        {
            wayPoint.previousWaypoint = waypointRoot.GetChild(waypointRoot.childCount - 2).GetComponent<WayPoint>();
            wayPoint.previousWaypoint.nextWaypoint = wayPoint;
            //placeWaypointAtTheLastPosition
            wayPoint.transform.position = wayPoint.previousWaypoint.transform.position;
            wayPoint.transform.forward = wayPoint.previousWaypoint.transform.forward;
        }
        Selection.activeGameObject = wayPoint.gameObject;
    }
    public void CreatWayPointBefore()
    {
        GameObject wayPointObj = new GameObject("Waypoint " + waypointRoot.childCount, typeof(WayPoint));
        wayPointObj.transform.SetParent(waypointRoot, false);
        WayPoint newWayPoint = wayPointObj.GetComponent<WayPoint>();
        WayPoint SelectedWayPoint = Selection.activeGameObject.GetComponent<WayPoint>();
        wayPointObj.transform.position = SelectedWayPoint.transform.position;
        wayPointObj.transform.forward = SelectedWayPoint.transform.forward;

        if (SelectedWayPoint.previousWaypoint != null)
        {
            newWayPoint.previousWaypoint = SelectedWayPoint.previousWaypoint;
            SelectedWayPoint.previousWaypoint.nextWaypoint = newWayPoint;
        }
        newWayPoint.nextWaypoint = SelectedWayPoint;
        SelectedWayPoint.previousWaypoint = newWayPoint;
        newWayPoint.transform.SetSiblingIndex(SelectedWayPoint.transform.GetSiblingIndex());
        Selection.activeGameObject = newWayPoint.gameObject;
    }
    public void CreatWayPointAfter()
    {
        GameObject wayPointObj = new GameObject("Waypoint " + waypointRoot.childCount, typeof(WayPoint));
        wayPointObj.transform.SetParent(waypointRoot, false);
        WayPoint newWayPoint = wayPointObj.GetComponent<WayPoint>();
        WayPoint SelectedWayPoint = Selection.activeGameObject.GetComponent<WayPoint>();
        wayPointObj.transform.position = SelectedWayPoint.transform.position;
        wayPointObj.transform.forward = SelectedWayPoint.transform.forward;

        newWayPoint.previousWaypoint = SelectedWayPoint;
        if (SelectedWayPoint.nextWaypoint != null)
        {
            SelectedWayPoint.nextWaypoint.previousWaypoint = newWayPoint;
            newWayPoint.nextWaypoint = SelectedWayPoint.nextWaypoint;
        }
        SelectedWayPoint.nextWaypoint = newWayPoint;
        newWayPoint.transform.SetSiblingIndex(SelectedWayPoint.transform.GetSiblingIndex());
        Selection.activeGameObject = newWayPoint.gameObject;
    }
    public void RemoveWayPoint()
    {
        WayPoint SelectedWayPoint = Selection.activeGameObject.GetComponent<WayPoint>();
        if (SelectedWayPoint.nextWaypoint != null)
        {
            SelectedWayPoint.nextWaypoint.previousWaypoint = SelectedWayPoint.previousWaypoint;
        }
        if (SelectedWayPoint.previousWaypoint != null)
        {
            SelectedWayPoint.previousWaypoint.nextWaypoint = SelectedWayPoint.nextWaypoint;
            Selection.activeGameObject = SelectedWayPoint.previousWaypoint.gameObject;
        }
        DestroyImmediate(SelectedWayPoint.gameObject);
    }

    public void CreatBranch()
    {
        GameObject wayPointObj = new GameObject("Waypoint " + waypointRoot.childCount, typeof(WayPoint));
        wayPointObj.transform.SetParent(waypointRoot, false);
        WayPoint wayPoint = wayPointObj.GetComponent<WayPoint>();
        WayPoint branchedFrom = Selection.activeGameObject.GetComponent<WayPoint>();
        branchedFrom.branches.Add(wayPoint);
        wayPoint.transform.position = branchedFrom.transform.position;
        wayPoint.transform.forward = branchedFrom.transform.forward;
        Selection.activeGameObject = wayPoint.gameObject;
    }
}

