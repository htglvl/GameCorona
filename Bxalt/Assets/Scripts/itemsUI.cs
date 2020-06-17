using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class itemsUI : MonoBehaviour
{
    public AspectRatioFitter ARF;
    public Image Content;
    public TextMeshProUGUI howManyNadeOnly;

    // Update is called once per frame
    public void UpdateARFRatio()
    {
        ARF.aspectRatio = Content.sprite.texture.width / Content.sprite.texture.height;
    }
    public void TextShowHowMany(string howMany)
    {
        howManyNadeOnly.text = howMany.ToString();
    }
    public void DeleteImage()
    {
        Content.sprite = null;
        Color temp = Content.color;
        temp.a = 0;
        Content.color = temp;
    }
    public void AddImage()
    {
        Color temp = Content.color;
        temp.a = 1;
        Content.color = temp;
    }

}
