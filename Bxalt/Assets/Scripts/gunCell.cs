using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[RequireComponent(typeof(Image))]
public class gunCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    #region Variable
    public Image backGround;
    public bool isGun = true;
    public int Price;
    public GameObject gunObject;
    public gunStats gunstats;
    public PlayerTopDownShooting weaponStats;
    public UnityEvent onTabSelected, onTabDeselected, onTabHover;
    #endregion

    public void OnPointerClick(PointerEventData eventData)
    {
        gunstats.OnTabSelected(this);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        gunstats.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gunstats.OnTabExit(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        backGround = GetComponent<Image>();
        gunstats.Subcribe(this);
    }
    public void Select()
    {
        if (onTabSelected != null)
        {
            onTabSelected.Invoke();
        }

    }
    public void Hover()
    {
        if (onTabHover != null)
        {
            onTabHover.Invoke();
        }

    }
    public void Deselect()
    {
        if (onTabDeselected != null)
        {
            onTabDeselected.Invoke();
        }
    }
}
