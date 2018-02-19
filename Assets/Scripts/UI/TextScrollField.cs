using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class TextScrollField : MonoBehaviour
{
    //top anchor is called for static text
    public RectTransform topAnchor;
    //bottom Anchor is called for scrolling text that is read
    public RectTransform bottomAnchor;
    public RectTransform rectToAnchor;
    public RectTransform dialogueBox;

    public float rate = 100f;

    [Range(-1f, 1f)]
    public float dragValue = 1f;

    public bool canDrag = false;
    public bool isTextBeingRead = false;

    //Anchors a rect to another rect that has a changing height.



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        dragValue = Mathf.Clamp(dragValue, -1f, 1f);
        float height = rectToAnchor.rect.height;
        float baseHeight = height / 2f;
        float percent = dragValue * baseHeight;
        float currentHeight = Mathf.Clamp(percent, -baseHeight + dialogueBox.rect.height / 2f, baseHeight);
        float currentRate = height / rate;
        rectToAnchor.position = new Vector3(rectToAnchor.position.x, (topAnchor.position.y - currentHeight));
        //rectToAnchor.rect.position.Set(rectToAnchor.position.x, (topAnchor.rect.position.y - currentHeight));
        if (canDrag == true)
        {
            dragValue += Input.GetAxis("Mouse ScrollWheel") * rate;
        }

    }

    public void SetDragValue(float value)
    {
        dragValue = value;
    }



    //idea, have text that reads from bottom to up, and or right to left to confuse the reader.
    private void OnDrawGizmos()
    {
        dragValue = Mathf.Clamp(dragValue, -1f, 1f);
        float height = rectToAnchor.rect.height;
        float baseHeight = height / 2f;
        float percent = dragValue * baseHeight;
        float currentHeight = Mathf.Clamp(percent, -baseHeight + dialogueBox.rect.height / 2f, baseHeight);
        float currentRate = height / rate;
        rectToAnchor.position = new Vector3(rectToAnchor.position.x, (topAnchor.position.y - currentHeight));
        //rectToAnchor.rect.position.Set(rectToAnchor.position.x, (topAnchor.rect.position.y - currentHeight));
        if (canDrag == true)
        {
            dragValue += Input.GetAxis("Mouse ScrollWheel") * rate;
        }
    }
}
