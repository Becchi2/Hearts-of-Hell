using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject HoverPanel;

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Code to execute when the pointer enters the UI element
        HoverPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Code to execute when the pointer exits the UI element
        HoverPanel.SetActive(false);
    }
}
