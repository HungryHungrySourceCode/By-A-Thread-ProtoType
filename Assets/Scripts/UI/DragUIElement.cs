using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragUIElement : MonoBehaviour
{
    public RectTransform uiTransform;
    Vector2 pos;

    private void Update()
    {

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            pos.y += Input.GetAxis("Mouse ScrollWheel") * 100f;
        }
    }
}
