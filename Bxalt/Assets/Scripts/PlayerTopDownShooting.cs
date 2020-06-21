using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class PlayerTopDownShooting : MonoBehaviour
{
    #region Variable 
    public bool haveSound = true, holdOneHandTrueTwoHandFalse;
    public string sound, Name;
    public string scopeSound = "ButtonHover";
    public string BurstSound = "Map&Buy";
    private Transform firepos;
    private Camera cam;
    public bool AutoTrueHayOnetapFalse, ChuotPhaiCoTacDung, chuotphaiChuyenModeTrueHayScopeFalse;
    public bool napDanTungVienHayCaBang;
    [HideInInspector]
    public bool Burst;
    public float firerate, reloadTime, weight; //fireRate should < 60 for raycast to work normal
    public float bulletlifetime;
    public int maxammo, defaultMagezine;
    [HideInInspector]
    public int currentammo, currentBulletInMagazine;
    public float rateofFireBetweenBurstbullets = 0.1f;
    public float burstFireRate;
    public GameObject ProjectilePrefab;
    private GameObject LeftSpreadIndicator, RightSpreadIndicator, CrossHair;
    public int NumberOfBulletsBurst = 3;
    private float nexttimetofire = 0;
    [HideInInspector]
    public bool isreloading = false;
    private bool ShotgunCanReloading;
    public int index;
    public bool RayCastTrueHayPrefabFalse;
    private LineRenderer[] lineRenderer;
    public float lengthRayCast;
    public GameObject impacteffect, EnemyBlood;
    public int damage; // Only for raycast;
    private GameObject MuzzleFlash;
    public float maxBulletSpreadAngle;
    public float timeTillMaxSpreadAngle, coolDownMultiplier;
    private float fireTime;
    private float currentSpread;
    private float currentSpreadZ;
    Quaternion randomRotation;
    public int soVienBanRa1Luc;
    public float dazeTime;
    public float kBForce, SpreadVelocityMultiplier = 1;
    private bool haveJustShot;
    [SerializeField]
    public LayerMask NotPlayer;
    [HideInInspector]
    public bool isreloadingShotgun;
    float velocityInFloat;
    public Color NormalSpreadIndicator, CantShootIndicator;
    private SpriteRenderer LeftIndicatorSprite, RightIndicatorSprite;
    private audiomanager audiomanager;
    float SpreadShotGunOrNormal(bool Left)
    {
        float ShotgunSpread = maxBulletSpreadAngle + (velocityInFloat * SpreadVelocityMultiplier / 2);
        float spread = soVienBanRa1Luc == 1 ? currentSpread : ShotgunSpread;
        return Left ? spread : -spread;
    }
    public Vector2 SpreadPos(bool Left)
    {
        float Distance = Vector2.Distance(transform.position, CrossHair.transform.position);
        float Angle = Mathf.Deg2Rad * (firepos.eulerAngles.z + SpreadShotGunOrNormal(Left) + 90f);
        return new Vector2(Distance * Mathf.Cos(Angle) + firepos.position.x, Distance * Mathf.Sin(Angle) + firepos.position.y);
    }
    #endregion
    private void OnEnable()
    {
        isreloading = false;
        ShotgunCanReloading = false;
        transform.parent.parent.GetComponent<PlayerTopDownMovement>().weight = weight;
    }
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        currentBulletInMagazine = defaultMagezine;
        foreach (Transform child in transform)
        {
            if (child.tag == "firePos") { firepos = child; }
            if (child.tag == "MuzzleFlash") { MuzzleFlash = child.gameObject; }

        }
        lineRenderer = GetComponentsInChildren<LineRenderer>();
        currentammo = maxammo;
        CrossHair = GameObject.FindGameObjectWithTag("CrossHair");
        LeftSpreadIndicator = GameObject.FindGameObjectWithTag("LeftCrossHair");
        RightSpreadIndicator = GameObject.FindGameObjectWithTag("RightCrossHair");
        LeftIndicatorSprite = LeftSpreadIndicator.GetComponentInChildren<SpriteRenderer>();
        RightIndicatorSprite = RightSpreadIndicator.GetComponentInChildren<SpriteRenderer>();
        audiomanager[] audiomanagers = FindObjectsOfType<audiomanager>();
        foreach (var audio in audiomanagers)
        {
            if (audio.OnlyOneCanBe == false)
            {
                audiomanager = audio;
            }
        }

    }
    void Update()
    {
        if (Time.timeScale > 0)
        {
            SpreadIndicator();
            if (ShotgunCanReloading && currentammo < maxammo && currentBulletInMagazine > 0 && haveJustShot == false && napDanTungVienHayCaBang)
            {
                ShotgunCanReloading = false;
                StartCoroutine(ReloadTungVien1());
                return;
            }
            if (isreloading == true && currentammo == maxammo)
            {
                isreloading = false;
                StopCoroutine(Reload());
            }
            if (isreloading == true)
            {
                return;
            }
            if (fireTime < 0)
            {
                fireTime = 0;
            }
            if (fireTime > timeTillMaxSpreadAngle)
            {
                fireTime = timeTillMaxSpreadAngle;
            }
            transform.position = transform.parent.parent.Find("Guns").position;
            transform.rotation = transform.parent.parent.Find("Guns").rotation;
            if (Input.GetButtonDown("Fire2" + index.ToString()) && ChuotPhaiCoTacDung)
            {
                if (chuotphaiChuyenModeTrueHayScopeFalse)
                {
                    Burst = !Burst;
                    audiomanager.Play(BurstSound);
                }
                else
                {
                    cam.GetComponent<CameraController>().scope = !cam.GetComponent<CameraController>().scope;
                    audiomanager.Play(scopeSound);
                }
            }
            if ((currentammo <= 0 && Input.GetButton("Fire1" + index.ToString())) || (Input.GetKeyDown(KeyCode.R) && currentammo < maxammo) && currentBulletInMagazine > 0)
            {
                if (!isreloadingShotgun)
                {
                    haveJustShot = false;
                    ShotgunCanReloading = true;
                }
                StartCoroutine(Reload());
                return;
            }
            if (AutoTrueHayOnetapFalse)
            {
                if (Burst)
                {
                    if (Input.GetButton("Fire1" + index.ToString()))
                    {
                        fireTime += Time.deltaTime;
                        if (Time.time >= nexttimetofire)
                        {
                            nexttimetofire = Time.time + 1 / burstFireRate;
                            StartCoroutine(BurstShoot());
                        }
                    }
                    else
                    {
                        fireTime -= Time.deltaTime * coolDownMultiplier;
                    }
                }
                else
                {
                    if (Input.GetButton("Fire1" + index.ToString()))
                    {
                        fireTime += Time.deltaTime;
                        if (Time.time >= nexttimetofire)
                        {
                            nexttimetofire = Time.time + 1 / firerate;
                            Shoot();
                        }
                    }
                    else
                    {
                        fireTime -= Time.deltaTime * coolDownMultiplier;
                    }
                }

            }
            else
            {
                if (Burst)
                {
                    if (Input.GetButtonDown("Fire1" + index.ToString()))
                    {
                        fireTime += Time.deltaTime;
                        if (Time.time >= nexttimetofire)
                        {
                            nexttimetofire = Time.time + 1 / burstFireRate;
                            StartCoroutine(BurstShoot());
                        }
                    }
                    else
                    {
                        fireTime -= Time.deltaTime * coolDownMultiplier;
                    }
                }
                else
                {
                    if (Input.GetButtonDown("Fire1" + index.ToString()))
                    {
                        fireTime += Time.deltaTime;
                        if (Time.time >= nexttimetofire)
                        {
                            nexttimetofire = Time.time + 1 / firerate;
                            Shoot();
                        }
                    }
                    else
                    {
                        fireTime -= Time.deltaTime * coolDownMultiplier;
                    }
                }
            }
        }
    }
    void Shoot()
    {
        if (haveSound)
        {
            audiomanager.Play(sound);
        }
        haveJustShot = true;
        ShotgunCanReloading = false;
        StopCoroutine(ReloadTungVien1());
        currentammo--;
        if (RayCastTrueHayPrefabFalse)
        {
            StartCoroutine(RaycastShoot());
        }
        else
        {
            StartCoroutine(PrefabShoot());
        }
        return;
    }
    IEnumerator BurstShoot()
    {
        if (haveSound)
        {
            audiomanager.Play(sound);
        }
        int burstBullet;
        haveJustShot = true;
        if (NumberOfBulletsBurst > currentammo)
        {
            burstBullet = currentammo;
        }
        else
        {
            burstBullet = NumberOfBulletsBurst;
        }
        for (int i = 0; i < burstBullet; i++)
        {
            currentammo--;
            if (haveSound)
            {
                audiomanager.Play(sound);

            }
            ShotgunCanReloading = false;
            StopCoroutine(ReloadTungVien1());
            if (RayCastTrueHayPrefabFalse)
            {
                StartCoroutine(RaycastShoot());
            }
            else
            {
                StartCoroutine(PrefabShoot());
            }
            yield return new WaitForSeconds(1 / rateofFireBetweenBurstbullets);
        }
    }
    IEnumerator ReloadTungVien1()
    {
        if (currentammo == 0)
        {
            isreloading = true;
        }
        else
        {
            isreloading = false;
        }
        if (haveJustShot == false)
        {
            isreloadingShotgun = true;
            yield return new WaitForSeconds(reloadTime);
            if (haveJustShot == false)
            {
                currentBulletInMagazine--;
                currentammo++;
            }
            ShotgunCanReloading = true;
            isreloadingShotgun = false;

        }
        else
        {
            StopCoroutine(ReloadTungVien1());
        }
    }
    IEnumerator Reload()
    {
        if (napDanTungVienHayCaBang == false)
        {
            isreloading = true;
            yield return new WaitForSeconds(reloadTime);
            if (isreloading == true)
            {
                if (currentBulletInMagazine > maxammo - currentammo)
                {
                    currentBulletInMagazine = currentBulletInMagazine - (maxammo - currentammo);
                    currentammo = maxammo;
                }
                else
                {
                    currentammo += currentBulletInMagazine;
                    currentBulletInMagazine = 0;
                }
                isreloading = false;
            }
        }
    }
    IEnumerator RaycastShoot()
    {
        for (int i = 0; i < soVienBanRa1Luc; i++)
        {
            if (lineRenderer.Length > 0)
            {
                lineRenderer[i].SetPosition(0, firepos.position);
            }
            if (i > 1)
            {
                CalculateSpreadShotgun();
            }
            else
            {
                CalculateSpread();
            }
            RaycastHit2D hitInfo = Physics2D.Raycast(firepos.position, randomRotation * firepos.up, lengthRayCast);
            RaycastHit2D fromPlayertoFirePos = Physics2D.Raycast(transform.position, firepos.position - transform.position, Vector2.Distance(firepos.position, transform.position), NotPlayer);
            if (fromPlayertoFirePos && fromPlayertoFirePos.transform.tag != "Player")
            {
                Enemy enemyClose = fromPlayertoFirePos.transform.GetComponent<Enemy>();
                if (enemyClose != null)
                {
                    enemyClose.TakeDamage(damage);
                    gameObject.GetComponentInParent<money>().TotalDamageDealt += damage;
                    objectPooler.Instance.SpawnFromPool("Impact Effect", fromPlayertoFirePos.point, Quaternion.LookRotation(fromPlayertoFirePos.normal)).GetComponent<ParticleSystem>().Play();
                    if (fromPlayertoFirePos.transform.gameObject.GetComponent<EnemyAttack>() != null)
                    {
                        fromPlayertoFirePos.transform.gameObject.GetComponent<EnemyAttack>().GotKnocked(kBForce, dazeTime);
                        objectPooler.Instance.SpawnFromPool("EnemyBlood", fromPlayertoFirePos.point, Quaternion.LookRotation(fromPlayertoFirePos.normal)).GetComponent<ParticleSystem>().Play();
                    }
                }
                AIBrain AI = hitInfo.transform.GetComponent<AIBrain>();
                if (AI != null)
                {
                    AI.GotHitBy(sound + Name, damage);
                }
            }
            if (hitInfo)
            {
                Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                    gameObject.GetComponentInParent<money>().TotalDamageDealt += damage;
                    objectPooler.Instance.SpawnFromPool("Impact Effect", hitInfo.point, Quaternion.LookRotation(hitInfo.normal)).GetComponent<ParticleSystem>().Play();
                    if (hitInfo.transform.gameObject.GetComponent<EnemyAttack>() != null)
                    {
                        hitInfo.transform.gameObject.GetComponent<EnemyAttack>().GotKnocked(kBForce, dazeTime);
                        objectPooler.Instance.SpawnFromPool("EnemyBlood", hitInfo.point, Quaternion.LookRotation(hitInfo.normal)).GetComponent<ParticleSystem>().Play();
                    }
                }
                AIBrain AI = hitInfo.transform.GetComponent<AIBrain>();
                if (AI != null)
                {
                    AI.GotHitBy(sound + Name, damage);
                }
                if (lineRenderer.Length > 0) { lineRenderer[i].SetPosition(1, hitInfo.point); }
            }
            else
            {
                if (lineRenderer.Length > 0)
                {
                    lineRenderer[i].SetPosition(1,
                    new Vector2(lengthRayCast * Mathf.Cos(Mathf.Deg2Rad * (firepos.eulerAngles.z + currentSpreadZ + 90f)) + firepos.position.x, lengthRayCast * Mathf.Sin(Mathf.Deg2Rad * (firepos.eulerAngles.z + currentSpreadZ + 90f)) + firepos.position.y));
                }
            }

        }
        if (lineRenderer.Length > 0)
        {
            for (int i = 0; i < lineRenderer.Length; i++)
            {
                lineRenderer[i].enabled = true;
            }
        }
        if (MuzzleFlash != null) { MuzzleFlash.SetActive(true); }
        yield return new WaitForSeconds(0.02f);
        if (lineRenderer.Length > 0)
        {
            for (int i = 0; i < lineRenderer.Length; i++)
            {
                lineRenderer[i].enabled = false;
            }
        }
        if (MuzzleFlash != null) { MuzzleFlash.SetActive(false); }

    }
    IEnumerator PrefabShoot()
    {
        for (int i = 0; i < soVienBanRa1Luc; i++)
        {
            if (i > 1)
            {
                CalculateSpreadShotgun();
            }
            else
            {
                CalculateSpread();
            }
            GameObject bullet = Instantiate(ProjectilePrefab, firepos.position, firepos.rotation * randomRotation);
            IpooledObject pooledObject = bullet.GetComponent<IpooledObject>();
            if (pooledObject != null)
            {
                pooledObject.OnObjectSpawn();
            }
            Destroy(bullet, bulletlifetime);
        }
        if (MuzzleFlash != null) { MuzzleFlash.SetActive(true); }
        yield return new WaitForSeconds(0.02f);
        if (MuzzleFlash != null) { MuzzleFlash.SetActive(false); }

    }
    void CalculateSpread()
    {
        currentSpreadZ = Random.Range(-currentSpread, currentSpread);
        randomRotation = Quaternion.Euler(0, 0, currentSpreadZ);
    }
    void CalculateSpreadShotgun()
    {
        float crntSprd = maxBulletSpreadAngle + (velocityInFloat * SpreadVelocityMultiplier / 2);
        currentSpreadZ = Random.Range(-crntSprd, crntSprd);
        randomRotation = Quaternion.Euler(0, 0, currentSpreadZ);
    }
    void SpreadIndicator()
    {
        CrossHair.transform.position = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, 0);
        CrossHair.transform.LookAt(transform.position);

        velocityInFloat = transform.parent.parent.GetComponent<Rigidbody2D>().velocity.magnitude;
        currentSpread = (velocityInFloat * SpreadVelocityMultiplier) + Mathf.Lerp(0.0f, maxBulletSpreadAngle, fireTime / timeTillMaxSpreadAngle);

        LeftSpreadIndicator.transform.position = SpreadPos(true);
        RightSpreadIndicator.transform.position = SpreadPos(false);
        if (Time.time >= nexttimetofire)
        {
            SpreadColor(true, NormalSpreadIndicator);
        }
        else
        {
            SpreadColor(false, NormalSpreadIndicator);
        }
        if (Vector2.Distance(transform.position, CrossHair.transform.position) > lengthRayCast && RayCastTrueHayPrefabFalse)
        {
            if (Time.time >= nexttimetofire)
            {
                SpreadColor(true, Color.red);
            }
            else
            {
                SpreadColor(false, Color.red);
            }
        }
        else
        {
            if (Time.time >= nexttimetofire)
            {
                SpreadColor(true, NormalSpreadIndicator);
            }
            else
            {
                SpreadColor(false, NormalSpreadIndicator);
            }
        }

    }
    void SpreadColor(bool canOrcant, Color normalColor)
    {
        Color currentColor;
        if (canOrcant)
        {
            currentColor = normalColor;
        }
        else
        {
            currentColor = CantShootIndicator;
        }
        RightIndicatorSprite.color = currentColor;
        LeftIndicatorSprite.color = currentColor;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, 0, -maxBulletSpreadAngle) * transform.up * lengthRayCast);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, 0, maxBulletSpreadAngle) * transform.up * lengthRayCast);
    }
}