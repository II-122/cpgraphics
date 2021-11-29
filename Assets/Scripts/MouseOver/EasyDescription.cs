using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EasyDescription : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public GameObject EasyMode;


    public void OnPointerEnter(PointerEventData eventData)          // 마우스가 난이도 선택 버튼 위에 있으면
    {
        EasyMode.SetActive(true);                                   // 난이도에 대한 추가설명 패널 띄우고
        Debug.Log("OnMouseOver");
    }

    public void OnPointerExit(PointerEventData eventData)           // 마우스가 버튼의 영역에서 벗어나면
    {
        EasyMode.SetActive(false);                                  // 난이도 설명 패널 숨기기
    }
}
