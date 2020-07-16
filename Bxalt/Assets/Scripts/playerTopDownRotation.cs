using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class playerTopDownRotation : MonoBehaviour
{
    private Camera cam;
    Vector2 dir;
    float angle;
    Quaternion rotation;
    public float offsetRotation = -90;
    private PhotonView PV;
    public bool CanControl = false;

    // Start is called before the first frame update
    void Start()
    {
        PV = gameObject.GetComponent<PhotonView>();
        if (PV == null)
        {
            CanControl = true;
        }
        else
        {
            if (PV.IsMine)
            {
                CanControl = true;
            }
        }
        if (CanControl)
        {
            cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (CanControl)
        {
            Rotation();
        }
    }
    void Rotation()
    {
        dir = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) + offsetRotation;
        rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10 * Time.deltaTime);
    }
}
