using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour, IpooledObject
{
    public float minspeed = 5f;
    public float maxspeed = 8f;
    public bool fromPlayer = false;
    public int damage = 12;
    public float force = 1, lifeTime;
    Rigidbody2D rb2D;
    private EnemyAttack EA;
    private Vector2 forceVector;
    // Start is called before the first frame update
    public void OnObjectSpawn()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.velocity = Vector2.zero;
        forceVector = transform.up * Random.Range(minspeed, maxspeed);
        rb2D.AddForce(forceVector, ForceMode2D.Impulse);
        disableOnTime(lifeTime);
    }
    // Update is called once per frame

    void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        Rigidbody2D rigid = other.GetComponent<Rigidbody2D>();
        EA = other.GetComponent<EnemyAttack>();

        if (fromPlayer)
        {
            if (other.tag != "BulletFromPlayer" && other.tag != "Player" && other.tag != "Coin")
            {

                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                    GameObject.FindObjectOfType<money>().TotalDamageDealt += damage;

                }
                if (EA != null)
                {
                    EA.GotKnocked(10f, 2f);

                    objectPooler.Instance.SpawnFromPool("EnemyBlood", transform.position, Quaternion.identity * Quaternion.Euler(0f, 90f, 0)).GetComponent<ParticleSystem>().Play();
                }
                this.gameObject.SetActive(false);
            }
            if (other.tag != "Bullet")
            {
                objectPooler.Instance.SpawnFromPool("Impact Effect", transform.position, Quaternion.identity * Quaternion.LookRotation(forceVector) * Quaternion.Euler(180f, 0f, 0)).GetComponent<ParticleSystem>().Play();
            }
        }
        else
        {
            if (other.tag != "Bullet" && other.tag != "Enemy" && other.tag != "Coin")
            {
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
                if (rigid != null)
                {
                    rigid.AddForce(transform.GetComponent<Rigidbody2D>().velocity * force, ForceMode2D.Force);
                }
                objectPooler.Instance.SpawnFromPool("Impact Effect", transform.position, Quaternion.identity * Quaternion.LookRotation(forceVector) * Quaternion.Euler(180f, 0f, 0)).GetComponent<ParticleSystem>().Play();
                if (other.CompareTag("Player"))
                {
                    objectPooler.Instance.SpawnFromPool("EnemyBlood", transform.position, Quaternion.identity * Quaternion.Euler(0, 90f, 0)).GetComponent<ParticleSystem>().Play();
                }
                this.gameObject.SetActive(false);
            }
        }
    }
    IEnumerator disableOnTime(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
