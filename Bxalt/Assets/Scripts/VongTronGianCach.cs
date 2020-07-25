using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VongTronGianCach : MonoBehaviour
{
    public float KCbatdauhienVongTron, KCopacityVongTrondu;
    public SpriteRenderer Circle;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(player.transform.position, transform.position) <= KCbatdauhienVongTron)
        {
            float a = (((KCbatdauhienVongTron - Vector2.Distance(player.transform.position, transform.position))) / (KCbatdauhienVongTron - KCopacityVongTrondu));
            Circle.color = new Color(Circle.color.r, Circle.color.g, Circle.color.b, a);
        }
        else
        {
            Circle.color = new Color(Circle.color.r, Circle.color.g, Circle.color.b, 0);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, KCbatdauhienVongTron);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, KCopacityVongTrondu);
    }
}
