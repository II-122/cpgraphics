using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EasyDescription : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public GameObject EasyMode;


    public void OnPointerEnter(PointerEventData eventData)
    {
        EasyMode.SetActive(true);
        Debug.Log("OnMouseOver");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        EasyMode.SetActive(false);
    }
}
