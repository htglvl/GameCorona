using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTopDownMovement : MonoBehaviour
{
    public int Index;
    private Rigidbody2D rigid;
    public float speed = 1, BoostSpeed; // for additional boost like alcoho,...
    [HideInInspector]
    public float weight;
    public Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal" + Index.ToString()), Input.GetAxisRaw("Vertical" + Index.ToString())).normalized;
        if (weight == 0)
        {
            Debug.Log("with that much weight he can never walk");
            weight = 1;
        }
    }
    private void FixedUpdate()
    {
        rigid.AddForce(direction * speed * weight * BoostSpeed * Time.fixedDeltaTime);
        //Debug.Log(rigid.velocity.magnitude);
        //rigid.MovePosition(rigid.position + direction * speed * BoostSpeed * Time.fixedDeltaTime);
    }
}
