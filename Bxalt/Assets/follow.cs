using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour
{
    public Transform followWho;
    // Update is called once per frame
    void Update()
    {
        transform.position = followWho.position;
    }
}
