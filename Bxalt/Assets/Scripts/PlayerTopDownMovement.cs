using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerTopDownMovement : MonoBehaviour
{
    public int Index;
    private Rigidbody2D rigid;
    public float speed = 1, BoostSpeed = 1; // for additional boost like alcoho,...
    [HideInInspector]
    public float weight;
    public Vector2 direction;
    private PhotonView PV = null;
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
            rigid = gameObject.GetComponent<Rigidbody2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CanControl)
        {
            direction = new Vector2(Input.GetAxisRaw("Horizontal" + Index.ToString()), Input.GetAxisRaw("Vertical" + Index.ToString())).normalized;
            if (weight == 0)
            {
                Debug.Log("with that much weight he can never walk");
                weight = 1;
            }
        }
    }
    private void FixedUpdate()
    {
        if (CanControl)
        {
            rigid.AddForce(direction * speed * weight * BoostSpeed * Time.fixedDeltaTime);
            //Debug.Log(rigid.velocity.magnitude);
            //rigid.MovePosition(rigid.position + direction * speed * BoostSpeed * Time.fixedDeltaTime);
        }
    }
}
