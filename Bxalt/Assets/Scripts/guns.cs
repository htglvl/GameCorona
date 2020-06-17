using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guns : MonoBehaviour
{
    private Transform firepos;
    public bool AutoTrueHayOnetapFalse, ChuotPhaiCoTacDung, chuotphaiChuyenModeTrueHayScopeFalse;
    private bool Burst, Scope;
    public float bulletforce, firerate, reloadTime;
    public float bulletlifetime;
    public float maxammo;
    private float currentammo;
    public float BulletBurstFireRate = 0.1f, FireRateBetweenBurst = 0.3f;
    public GameObject ProjectilePrefab;
    public int NumberOfBulletsBurst = 3;
    private float nexttimetofire = 0;
    private bool isreloading = false;

    private void OnEnable()
    {
        isreloading = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentammo = maxammo;
        foreach (Transform child in transform)
        {
            if (child.tag == "firepoint")
            {
                firepos = child;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.parent.parent.Find("weaponpoint").position;
        transform.rotation = transform.parent.parent.Find("weaponpoint").rotation;
        if (Input.GetButtonDown("Fire2") && ChuotPhaiCoTacDung)
        {
            if (chuotphaiChuyenModeTrueHayScopeFalse)
            {
                Burst = !Burst;
            }
            else
            {
                Scope = !Scope;
            }
        }
        if (isreloading == true)
        {
            return;
        }
        if (currentammo <= 0 && Input.GetButton("Fire1"))
        {
            StartCoroutine(Reload());
            return;

        }
        if (AutoTrueHayOnetapFalse)
        {
            if (Burst)
            {
                if (Input.GetButtonDown("Fire1") && Time.time >= nexttimetofire)
                {
                    nexttimetofire = Time.time + 1 / firerate;
                    StartCoroutine(BurstShoot());
                }
            }
            else
            {
                if (Input.GetButton("Fire1") && Time.time >= nexttimetofire)
                {
                    nexttimetofire = Time.time + 1 / firerate;
                    Shoot();
                    Debug.Log("shot");
                }
            }

        }
        else
        {
            if (Burst)
            {
                if (Input.GetButtonDown("Fire1") && Time.time >= nexttimetofire)
                {
                    nexttimetofire = Time.time + 1 / firerate;
                    StartCoroutine(BurstShoot());
                }
            }
            else
            {
                if (Input.GetButtonDown("Fire1") && Time.time >= nexttimetofire)
                {
                    nexttimetofire = Time.time + 1 / firerate;
                    Shoot();
                    Debug.Log("shot");
                }
            }
        }
    }
    void Shoot()
    {
        currentammo--;
        GameObject bullet = Instantiate(ProjectilePrefab, firepos.position, firepos.rotation);
        Destroy(bullet, bulletlifetime);
        Rigidbody2D rbullet = bullet.GetComponent<Rigidbody2D>();
        rbullet.AddForce(firepos.up * bulletforce, ForceMode2D.Impulse);
    }

    IEnumerator BurstShoot()
    {
        for (int i = 0; i < NumberOfBulletsBurst; i++)
        {
            currentammo--;
            GameObject bullet = Instantiate(ProjectilePrefab, firepos.position, firepos.rotation);
            Destroy(bullet, bulletlifetime);
            Rigidbody2D rbullet = bullet.GetComponent<Rigidbody2D>();
            rbullet.AddForce(firepos.up * bulletforce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(BulletBurstFireRate);
        }
    }
    IEnumerator Reload()
    {
        Debug.Log("reloading");
        isreloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentammo = maxammo;
        isreloading = false;
    }

}
