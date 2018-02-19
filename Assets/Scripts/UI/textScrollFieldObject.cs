using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class textScrollFieldObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextScrollField currentScrollField;

    public void OnPointerEnter(PointerEventData eventData)
    {
        currentScrollField.canDrag = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        currentScrollField.canDrag = false;
    }
}
