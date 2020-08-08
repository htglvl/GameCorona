using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class gunStats : MonoBehaviour
{
    public List<gunCell> guns;
    public GameObject player;
    public TextMeshProUGUI Name, price, Damage, FullAutoOrSemi, ReloadTime, ScopeAndBurstFire, Ammo, WeaponFireRate, WeaponSlowDown, RayCastOrProjectile;
    public Color idleColor, hightlightedColor, activedColor;
    public itemsUI itemui;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void Subcribe(gunCell guncell)
    {
        if (guns == null)
        {
            guns = new List<gunCell>();
        }
        guns.Add(guncell);
    }
    public void OnTabEnter(gunCell gun)
    {
        gun.Hover();
        resetTabs();
        gun.backGround.color = hightlightedColor;
        price.text = "Price: " + gun.Price.ToString();
        if (gun.isGun)
        {
            Name.text = gun.weaponStats.gameObject.name;
            Damage.text = "Damage: " + (gun.weaponStats.RayCastTrueHayPrefabFalse ? gun.weaponStats.damage.ToString() : gun.weaponStats.ProjectilePrefab.GetComponent<bullet>().damage.ToString());
            FullAutoOrSemi.text = gun.weaponStats.AutoTrueHayOnetapFalse ? "Full Auto" : "Semi Auto";
            ReloadTime.text = "Reload Time: " + gun.weaponStats.reloadTime.ToString() + ", (" + (gun.weaponStats.napDanTungVienHayCaBang ? "Reload Từng viên" : "Reload cả băng") + ")";
            if (gun.weaponStats.ChuotPhaiCoTacDung)
            {
                if (gun.weaponStats.chuotphaiChuyenModeTrueHayScopeFalse)
                {
                    ScopeAndBurstFire.text = "Burst Fire";
                }
                else
                {
                    ScopeAndBurstFire.text = "Scope";
                }
            }
            else
            {
                ScopeAndBurstFire.text = "Scope: ko, Burst Fire: ko";
            }
            Ammo.text = "Ammo: " + gun.weaponStats.maxammo.ToString() + "/" + gun.weaponStats.defaultMagezine;
            WeaponFireRate.text = gun.weaponStats.firerate.ToString("0.00") + " Viên/giây";
            WeaponSlowDown.text = "Tốc độ giảm " + (1 - gun.weaponStats.weight).ToString("0.00") + "%";
            RayCastOrProjectile.text = gun.weaponStats.RayCastTrueHayPrefabFalse ? "Raycast" : "Projectiles";
        }
        else
        {
            Name.text = gun.gunObject.name;
            Damage.text = gun.gunObject.GetComponent<items>().mieuTa;
            FullAutoOrSemi.text = null;
            ReloadTime.text = null;
            ScopeAndBurstFire.text = null;
            Ammo.text = null;
            WeaponFireRate.text = null;
            WeaponSlowDown.text = null;
            RayCastOrProjectile.text = null;
        }
    }
    public void OnTabExit(gunCell gun)
    {
        resetTabs();
    }
    public void OnTabSelected(gunCell gun)
    {
        gun.Select();
        if (player.GetComponent<money>().wallet >= gun.Price)
        {
            player.GetComponent<money>().wallet -= gun.Price;
            resetTabs();
            if (gun.isGun)
            {
                if (player.GetComponentInChildren<PlayerTopDownShooting>().name == gun.weaponStats.name || player.GetComponentInChildren<PlayerTopDownShooting>().name == gun.weaponStats.name + "(Clone)")
                {
                    player.GetComponentInChildren<PlayerTopDownShooting>().currentBulletInMagazine = gun.weaponStats.defaultMagezine + player.GetComponentInChildren<PlayerTopDownShooting>().currentBulletInMagazine;
                }
                else
                {
                    Transform gunHolder = player.GetComponentInChildren<PlayerTopDownShooting>().transform.parent;
                    player.GetComponentInChildren<PlayerTopDownShooting>().gameObject.SetActive(false);
                    Instantiate(gun.weaponStats.gameObject, gunHolder);
                    GameObject.FindObjectOfType<CameraController>().scope = false;

                }
            }
            else
            {
                Transform itemHolder = GameObject.FindGameObjectWithTag("itemHolder").transform;
                if (itemHolder.childCount > 0)
                {
                    for (int i = 0; i < itemHolder.childCount; i++)
                    {
                        Destroy(itemHolder.GetChild(i).gameObject);
                        itemui.DeleteImage();
                    }
                }
                Instantiate(gun.gunObject, itemHolder);
                Sprite tempimage = gun.transform.GetChild(0).GetComponent<Image>().sprite;
                itemui.Content.sprite = tempimage;
                itemui.AddImage();
                itemui.UpdateARFRatio();
            }
        }
    }
    public void resetTabs()
    {
        foreach (gunCell gun in guns)
        {
            gun.backGround.color = idleColor;
            if (player.GetComponent<money>().wallet < gun.Price)
            {
                gun.backGround.color = new Color(1, 0, 0, gun.backGround.color.a);
            }
        }
    }
}
