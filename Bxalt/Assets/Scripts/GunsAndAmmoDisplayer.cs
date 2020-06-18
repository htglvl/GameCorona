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
    public Image Scope, Burst, sanitizer, soap, syring, mask, thermo, nothing, saniFore, soapFore, syringFore, MaskFore, ThermoFore, nothingFore;
    Image currentAmmoType;
    public GameObject ImageChild;
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
                    updateImage(Player.sound);

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
                    updateImage(Player.sound);
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
    void updateImage(string sound)
    {
        foreach (Image image in ImageChild.GetComponentsInChildren<Image>())
        {
            image.enabled = false;
        }
        if (sound == "sy")
        {
            syring.enabled = true;
            syringFore.enabled = true;
            currentAmmoType = syringFore;
        }
        else if (sound == "sani")
        {
            saniFore.enabled = true;
            sanitizer.enabled = true;
            currentAmmoType = saniFore;
        }
        else if (sound == "thermo")
        {
            thermo.enabled = true;
            ThermoFore.enabled = true;
            currentAmmoType = ThermoFore;
        }
        else if (sound == "soap")
        {
            soap.enabled = true;
            soapFore.enabled = true;
            currentAmmoType = soapFore;
        }
        else if (sound == "mask")
        {
            mask.enabled = true;
            MaskFore.enabled = true;
            currentAmmoType = MaskFore;
        }
        else
        {
            nothing.enabled = true;
            nothingFore.enabled = true;
            currentAmmoType = nothingFore;
        }
    }
}
