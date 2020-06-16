using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iconDisplayWhenPlay : MonoBehaviour
{
    public GameObject ObjectToSetActiveOnStart;
    void Start()
    {
        ObjectToSetActiveOnStart.SetActive(true);
    }
}
