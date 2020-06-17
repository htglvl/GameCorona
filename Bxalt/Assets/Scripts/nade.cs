using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nade : MonoBehaviour
{
    public bool isNade, isFlash;
    public float stoppingsecond;
    private Rigidbody2D rb2d;
    public GameObject Explosion;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.AddForce(((Vector2)(transform.up * Vector2.Distance(transform.position, cam.ScreenToWorldPoint(Input.mousePosition))) + GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity) * rb2d.drag, ForceMode2D.Impulse);
        StartCoroutine(Explod(stoppingsecond));

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player")
        {
            StartCoroutine(Explod(0.5f));
        }
    }
    IEnumerator Explod(float time)
    {
        transform.position = this.transform.position;
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
        if (isNade)
        {
            GameObject TempParticle = Instantiate(Explosion, transform.position, transform.rotation);
            Destroy(TempParticle, 1f);
        }
    }
}
