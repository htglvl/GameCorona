using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GunsAndAmmoDisplayer : MonoBehaviour
{
    public TextMeshProUGUI gunName, gunAmmo;
    public PlayerTopDownShooting Player;
    CameraController Camera;
    public Image Scope, Burst, Reloading1Vien, ReloadCaBang, reloadCaBangBackground, reload1VienBackGround;
    Image currentAmmoType;
    private float currentReloadingTime;
    bool StillAlive = true;

    // Update is called once per frame
    private void Start()
    {
        Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
    }
    void Update()
    {
        if (StillAlive)
        {
            if (Player != null)
            {
                //string OnlyName = Player.gameObject.name;
                gunName.text = Player.gameObject.name.Replace("(Clone)", "");
                gunAmmo.text = Player.currentammo.ToString() + "/" + Player.currentBulletInMagazine.ToString();
                if (Player.ChuotPhaiCoTacDung)
                {
                    if (Player.chuotphaiChuyenModeTrueHayScopeFalse)
                    {
                        Scope.enabled = false;
                        Burst.enabled = true;
                        if (Player.Burst)
                        {
                            Burst.transform.localScale = new Vector3(1, 1, 1);
                        }
                        else
                        {
                            Burst.transform.localScale = new Vector3(.75f, .75f, .75f);
                        }
                    }
                    else
                    {
                        Scope.enabled = true;
                        Burst.enabled = false;
                        if (Camera.scope)
                        {
                            Scope.transform.localScale = new Vector3(1, 1, 1);
                        }
                        else
                        {
                            Scope.transform.localScale = new Vector3(.75f, .75f, .75f);
                        }
                    }
                }
                else
                {
                    Scope.enabled = false;
                    Burst.enabled = false;
                }
                if (Player.napDanTungVienHayCaBang)
                {
                    Reloading1Vien.enabled = true;
                    reload1VienBackGround.enabled = true;
                    currentAmmoType = Reloading1Vien;
                    ReloadCaBang.enabled = false;
                    reloadCaBangBackground.enabled = false;
                    if (Player.isreloadingShotgun)
                    {
                        currentReloadingTime += Time.deltaTime;
                        currentAmmoType.fillAmount = currentReloadingTime / Player.reloadTime;
                        if (currentReloadingTime > Player.reloadTime)
                        {
                            currentReloadingTime = 0;
                        }
                    }
                    else
                    {
                        currentReloadingTime = 0;
                        currentAmmoType.fillAmount = (float)Player.currentammo / (float)Player.maxammo;
                    }

                }
                else
                {
                    Reloading1Vien.enabled = false;
                    reload1VienBackGround.enabled = false;
                    ReloadCaBang.enabled = true;
                    reloadCaBangBackground.enabled = true;
                    currentAmmoType = ReloadCaBang;
                    if (Player.isreloading)
                    {
                        currentReloadingTime += Time.deltaTime;
                        currentAmmoType.fillAmount = currentReloadingTime / Player.reloadTime;
                        //Debug.Log(currentReloadingTime + "/" + Player.reloadTime + "=" + currentReloadingTime / Player.reloadTime);
                    }
                    else
                    {
                        currentReloadingTime = 0;
                        currentAmmoType.fillAmount = (Player.currentammo * 1.0f) / (Player.maxammo * 1.0f);

                    }
                }
            }
            else
            {
                GameObject Character = GameObject.FindGameObjectWithTag("Player");
                if (Character != null)
                {
                    Player = Character.GetComponentInChildren<PlayerTopDownShooting>();
                }
                else
                {
                    StillAlive = false;
                }
            }
        }
    }
}
