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
    }
    public void CreatWayPoint()
    {
        GameObject wayPointObj = new GameObject("Waypoint " + waypointRoot.childCount, typeof(WayPoint));
        wayPointObj.transform.SetParent(waypointRoot, false);
        WayPoint wayPoint = wayPointObj.GetComponent<WayPoint>();
        if (waypointRoot.childCount > 1)
        {

        }
    }
}

