using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyMenuAndHotkey : MonoBehaviour
{
    public GameObject BuyMenu, fullMap;
    public TabGroup tab;
    public gunStats gun;
    public int i;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            tab.selectedTab = null;
            BuyMenu.SetActive(!BuyMenu.activeInHierarchy);
            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<audiomanager>().Play("Map&Buy");
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            fullMap.SetActive(!fullMap.activeInHierarchy);
            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<audiomanager>().Play("Map&Buy");
        }
        if (fullMap.activeInHierarchy || BuyMenu.activeInHierarchy)
        {
            Time.timeScale = 0;
        }
        else
        {
            if (GetComponent<GameManager>().gameIsPause == false && !Input.GetKey(KeyCode.Space)/*dung cho slowTime*/)
            {
                Time.timeScale = 1;
            }
        }
        if (BuyMenu.activeInHierarchy == true)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) { i = 1; }
            if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) { i = 2; }
            if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)) { i = 3; }
            if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4)) { i = 4; }
            if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5)) { i = 5; }
            if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6)) { i = 6; }
            if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7)) { i = 7; }
            if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8)) { i = 8; }
            if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9)) { i = 9; }
            if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0)) { i = 10; }
            if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus)) { i = 11; }
            if (Input.GetKeyDown(KeyCode.Equals) || Input.GetKeyDown(KeyCode.Plus)) { i = 12; }

            if (i > 0)
            {
                if (tab.selectedTab == null)
                {
                    if (i <= tab.objectsToSwap.Count)
                    {
                        SelectTab(i);
                        i = 0;
                    }
                }
                else
                {
                    SelectGun(i);
                    BuyMenu.SetActive(false);
                    i = 0;
                    tab.selectedTab = null;
                }
            }
        }
    }

    void SelectTab(int Index) { tab.OnTabSelected(tab.transform.GetChild(Index - 1).GetComponent<TabButton>()); }
    void SelectGun(int Index)
    {
        for (int i = 0; i < tab.objectsToSwap.Count; i++)
        {
            if (tab.objectsToSwap[i].activeInHierarchy && Index <= tab.objectsToSwap[i].transform.childCount)
            {
                gun.OnTabSelected(tab.objectsToSwap[i].transform.GetChild(Index - 1).GetComponent<gunCell>());
            }
        }
    }
}
