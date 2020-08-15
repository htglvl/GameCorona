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
    public float weight, horizontal4Anim, vertical4Anim, speed4Anim;
    public Vector2 direction;
    private PhotonView PV = null;
    public bool CanControl = false;
    public Animator charactor, mask, shield;
    public Transform Canvas, collider4Wall, ColliderCha;

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
        Animate();
        if (CanControl)
        {
            direction = new Vector2(Input.GetAxisRaw("Horizontal" + Index.ToString()), Input.GetAxisRaw("Vertical" + Index.ToString())).normalized;
            if (weight == 0)
            {
                Debug.Log("with that much weight he can never walk");
                weight = 1;
            }
        }
        charactor.transform.LookAt(charactor.transform.position + Vector3.forward);
        mask.transform.LookAt(mask.transform.position + Vector3.forward);
        shield.transform.LookAt(shield.transform.position + Vector3.forward);
        Canvas.LookAt(Canvas.transform.position + Vector3.forward);
        collider4Wall.LookAt(collider4Wall.transform.position + Vector3.forward);
        ColliderCha.LookAt(ColliderCha.transform.position + Vector3.forward);


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
    void Animate()
    {
        horizontal4Anim = Input.GetAxisRaw("Horizontal" + Index.ToString());
        vertical4Anim = Input.GetAxisRaw("Vertical" + Index.ToString());
        speed4Anim = Mathf.Clamp((direction * speed * weight * BoostSpeed * Time.deltaTime).magnitude, 0, 1);
        charactor.SetFloat("Horizontal", horizontal4Anim);
        charactor.SetFloat("Vertical", vertical4Anim);
        charactor.SetFloat("Speed", speed4Anim);
        mask.SetFloat("Horizontal", horizontal4Anim);
        mask.SetFloat("Vertical", vertical4Anim);
        mask.SetFloat("Speed", speed4Anim);
        shield.SetFloat("Horizontal", horizontal4Anim);
        shield.SetFloat("Vertical", vertical4Anim);
        shield.SetFloat("Speed", speed4Anim);
    }
    public void sync()
    {
        charactor.Play("Blend Tree", -1, 0);
        mask.Play("Blend Tree", -1, 0);
        shield.Play("Blend Tree", -1, 0);
    }
}
