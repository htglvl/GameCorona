using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightForHand : MonoBehaviour
{
    SpriteRenderer HandSprite;
    GameObject Light;
    // Start is called before the first frame update
    void Start()
    {
        HandSprite = GetComponent<SpriteRenderer>();
        Light = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (HandSprite.enabled == false)
        {
            if (Light.activeSelf == true)
            {
                Light.SetActive(false);
            }
        }
        else
        {
            if (Light.activeSelf == false)
            {
                Light.SetActive(true);
            }
        }
    }
}
