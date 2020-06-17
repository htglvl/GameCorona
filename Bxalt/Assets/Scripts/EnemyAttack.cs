using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public bool haveSound;
    public string shotSound;
    private Rigidbody2D rb2D;
    public float speed;
    public float healthBeforeRaiseDistance = 30;
    public float startTimeBtwShot;
    public float distanceRaiseMultiplier = 1.25f;
    public float stoppingDistance;
    public float retreatDistance;
    private float privateDistanceRaiseMultiplier;
    private Transform player;
    private float distanceFromPlayerToEnemy;
    private float timeBtwShot;
    float angle;
    public Transform firePoint;
    public float offsetRotation = -90f;
    Quaternion rotation;
    Vector2 dir;
    public bool enemyShoot;
    public GameObject Explosion;
    public int Pistol0Burst1Shotgun2 = 0;
    public int soDanShotgun = 6;
    public float timeBtwBurst = .5f;
    public int burstBullet = 3;
    public float minspread = 0, maxspread = 0;
    public float stoppingsecond;
    private float localKnockBackForce;
    private bool knockBack;
    private float startDazeTime;
    private audiomanager audiomanager;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShot = startTimeBtwShot;
        privateDistanceRaiseMultiplier = 1;
        audiomanager[] audiomanagers = FindObjectsOfType<audiomanager>();
        foreach (var audio in audiomanagers)
        {
            if (audio.OnlyOneCanBe == false)
            {
                audiomanager = audio;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            dir = player.position - transform.position;
            angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) + offsetRotation;
            rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10 * Time.deltaTime);
            if (gameObject.GetComponent<Enemy>() != null)
            {
                if (gameObject.GetComponent<Enemy>().health < healthBeforeRaiseDistance)
                {
                    privateDistanceRaiseMultiplier = distanceRaiseMultiplier;
                }
                else
                {
                    privateDistanceRaiseMultiplier = 1;
                }
            }
            distanceFromPlayerToEnemy = Vector2.Distance(transform.position, player.position);

            if (enemyShoot && distanceFromPlayerToEnemy < stoppingDistance * 2 * privateDistanceRaiseMultiplier && distanceFromPlayerToEnemy > retreatDistance * privateDistanceRaiseMultiplier)
            {
                if (startDazeTime <= 0)
                {
                    if (Pistol0Burst1Shotgun2 == 0)
                    {
                        if (timeBtwShot <= 0)
                        {
                            soundPlay();
                            objectPooler.Instance.SpawnFromPool("Rocket", firePoint.position, firePoint.rotation * Quaternion.Euler(0f, 0f, Random.Range(minspread, maxspread)));
                            timeBtwShot = startTimeBtwShot;
                        }
                        else
                        {
                            timeBtwShot -= Time.deltaTime;
                        }
                    }
                    else if (Pistol0Burst1Shotgun2 == 1)
                    {
                        if (timeBtwShot <= 0)
                        {
                            soundPlay();
                            StartCoroutine(Burst());
                            timeBtwShot = startTimeBtwShot;
                        }
                        else
                        {
                            timeBtwShot -= Time.deltaTime;
                        }
                    }
                    else
                    {
                        if (timeBtwShot <= 0)
                        {
                            soundPlay();
                            for (int i = 0; i < soDanShotgun; i++)
                            {

                                objectPooler.Instance.SpawnFromPool("Bullet", firePoint.position, firePoint.rotation * Quaternion.Euler(0f, 0f, Random.Range(minspread, maxspread)));
                                timeBtwShot = startTimeBtwShot;
                            }
                            timeBtwShot = startTimeBtwShot;
                        }
                        else
                        {
                            timeBtwShot -= Time.deltaTime;
                        }
                    }
                }
            }
            else
            {
                if (distanceFromPlayerToEnemy < stoppingDistance * privateDistanceRaiseMultiplier && distanceFromPlayerToEnemy > retreatDistance * privateDistanceRaiseMultiplier)
                {
                    StartCoroutine(Explod());
                }
            }
        }
    }
    private void FixedUpdate()
    {
        if (player != null)
        {
            if (startDazeTime > 0)
            {
                rb2D.velocity = Vector2.zero;
                startDazeTime -= Time.deltaTime;
            }
            if (knockBack)
            {
                rb2D.velocity = (player.position - transform.position).normalized * -localKnockBackForce;
                knockBack = false;
            }
            else
            {
                if (distanceFromPlayerToEnemy > stoppingDistance * privateDistanceRaiseMultiplier)
                {
                    rb2D.velocity = (player.position - transform.position).normalized * speed * Time.fixedDeltaTime;
                }
                else if (distanceFromPlayerToEnemy < stoppingDistance * privateDistanceRaiseMultiplier && distanceFromPlayerToEnemy > retreatDistance * privateDistanceRaiseMultiplier)
                {
                    rb2D.velocity = Vector2.zero;
                }
                else if (distanceFromPlayerToEnemy < retreatDistance * privateDistanceRaiseMultiplier)
                {
                    rb2D.velocity = (player.position - transform.position).normalized * -speed * Time.fixedDeltaTime;
                }
            }
        }
    }
    IEnumerator Burst()
    {
        for (int i = 0; i < burstBullet; i++)
        {
            objectPooler.Instance.SpawnFromPool("Bullet", firePoint.position, firePoint.rotation * Quaternion.Euler(0f, 0f, Random.Range(minspread, maxspread)));
            timeBtwShot = startTimeBtwShot;
        }
        yield return new WaitForSeconds(timeBtwBurst);
    }
    IEnumerator Explod()
    {
        transform.position = this.transform.position;
        yield return new WaitForSeconds(stoppingsecond);
        Destroy(gameObject);
        GameObject TempParticle = Instantiate(Explosion, transform.position, transform.rotation);
        Destroy(TempParticle, 1f);
    }
    public void GotKnocked(float knockBackForce, float dazedtimelocal)
    {
        startDazeTime = dazedtimelocal;
        localKnockBackForce = knockBackForce;
        knockBack = true;
    }
    void soundPlay()
    {
        audiomanager.Play(shotSound);
    }
}
