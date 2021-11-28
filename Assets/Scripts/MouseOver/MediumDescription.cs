using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MediumDescription : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public GameObject MediumMode;


    public void OnPointerEnter(PointerEventData eventData)
    {
        MediumMode.SetActive(true);
        Debug.Log("OnMouseOver");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MediumMode.SetActive(false);
    }
}