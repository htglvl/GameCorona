using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTopDownRotation : MonoBehaviour
{
    private Camera cam;
    Vector2 dir;
    float angle;
    Quaternion rotation;
    public float offsetRotation = -90;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotation();
    }
    void Rotation()
    {
        dir = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) + offsetRotation;
        rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10 * Time.deltaTime);
    }
}
