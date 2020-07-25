using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeSlow : MonoBehaviour
{
    void Update()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            Time.timeScale *= 0.1f;
        }
        if (Input.GetKey(KeyCode.Period))
        {
            Time.timeScale = 15;
        }

    }
}
