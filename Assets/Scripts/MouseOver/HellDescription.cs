using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HellDescription : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public GameObject HellMode;


    public void OnPointerEnter(PointerEventData eventData)
    {
        HellMode.SetActive(true);
        Debug.Log("OnMouseOver");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HellMode.SetActive(false);
    }
}