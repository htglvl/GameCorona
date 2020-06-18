using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handShow : MonoBehaviour
{
    public SpriteRenderer handLeft, handRight, OneHand, TwoHandLeft, twoHandRight;
    public GameObject gun;
    PlayerTopDownShooting preGun, currentGun;
    // Start is called before the first frame update
    private void Start()
    {
        disableAll();
        handLeft.enabled = true;
        handRight.enabled = true;
    }
    void Update()
    {
        preGun = currentGun;
        currentGun = gun.GetComponentInChildren<PlayerTopDownShooting>();
        if (preGun != currentGun)
        {
            disableAll();
            if (currentGun)
            {
                Debug.Log("havetop");
                if (gun.transform.GetChild(0).gameObject.name == "Tay Không")
                {
                    Debug.Log("tk");
                    handLeft.enabled = true;
                    handRight.enabled = true;
                }
                else
                {
                    Debug.Log("cogun");
                    if (gun.GetComponentInChildren<PlayerTopDownShooting>().holdOneHandTrueTwoHandFalse)
                    {
                        Debug.Log("oh");
                        OneHand.enabled = true;
                    }
                    else
                    {
                        Debug.Log("full");
                        twoHandRight.enabled = true;
                        TwoHandLeft.enabled = true;
                    }
                }
            }
        }
    }
    void disableAll()
    {
        handLeft.enabled = false;
        handRight.enabled = false;
        OneHand.enabled = false;
        TwoHandLeft.enabled = false;
        twoHandRight.enabled = false;
    }
}
