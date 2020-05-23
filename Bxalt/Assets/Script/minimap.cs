using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimap : MonoBehaviour
{
    public Transform Player;
    private void LateUpdate()
    {
        if (Player != null)
        {
            Vector3 newPos = Player.position;
            newPos.z = transform.position.z;
            transform.position = newPos;
        }
        else
        {
            if (GameObject.FindGameObjectWithTag("Player") != null)
            {
                Player = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }
    }
}
