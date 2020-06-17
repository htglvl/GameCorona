using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour, IpooledObject
{
    public float speed, damageRange;
    public int ObstacleDamage;
    private Transform player;
    private Vector2 target;
    public GameObject Explosion, RocketCrossHair;
    private GameObject CrossHairClone;
    public LayerMask whoIsPlayer;
    // Start is called before the first frame update
    public void OnObjectSpawn()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
        CrossHairClone = objectPooler.Instance.SpawnFromPool("RCrossHair", target, Quaternion.identity);

    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyProjectile();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Enemy")
        {
            DestroyProjectile();
            if (other.GetComponent<Enemy>() != null && other.tag != "Player")
            {
                other.GetComponent<Enemy>().TakeDamage(ObstacleDamage);
            }
        }
    }
    void DestroyProjectile()
    {
        Collider2D[] ThingsGotDamage = Physics2D.OverlapCircleAll(transform.position, damageRange, whoIsPlayer);
        for (int i = 0; i < ThingsGotDamage.Length; i++)
        {
            float distanceFromExplo = Vector2.Distance(transform.position, ThingsGotDamage[i].transform.position);
            int damageCal = Mathf.RoundToInt(
                  (ObstacleDamage - (((distanceFromExplo < damageRange) ? (distanceFromExplo / damageRange) : 1) * ObstacleDamage)));
            ThingsGotDamage[i].GetComponent<Enemy>().TakeDamage(damageCal);
        }
        GameObject TempParticle = Instantiate(Explosion, transform.position, Quaternion.identity);
        Destroy(TempParticle, 3f);
        CrossHairClone.SetActive(false);
        gameObject.SetActive(false);
    }
}
