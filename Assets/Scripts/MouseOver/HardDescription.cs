using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HardDescription : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public GameObject HardMode;


    public void OnPointerEnter(PointerEventData eventData)
    {
        HardMode.SetActive(true);
        Debug.Log("OnMouseOver");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HardMode.SetActive(false);
    }
}