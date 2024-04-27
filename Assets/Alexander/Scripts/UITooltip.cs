using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// simple tooltip manager that displays information from tooltip triggers
public class UITooltip : MonoBehaviour
{
    // needs a reference to accurately track mouse position
    private RectTransform _canvas;
    // reference to the ui body of the tooltip
    [SerializeField] private RectTransform _tooltipBody;
    // reference to the text of the tooltip
    [SerializeField] private TextMeshProUGUI _tooltipHeader;
    // reference to the text of the tooltip
    [SerializeField] private TextMeshProUGUI _tooltipText;
    // only one insyance can exist at a time
    private static UITooltip s_instance;
    [SerializeField] private Vector2 mouseOffset;


    // Start is called before the first frame update
    void Awake()
    {
        _canvas = transform.parent.GetComponent<RectTransform>();
        s_instance = this;
        SetText(null);
    }

    // follow mouse every frame
    public void Update()
    {     
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas, Input.mousePosition, Camera.main, out pos);
        SetDimensions(_canvas.transform.TransformPoint(pos));
    }

    // sets the text, or hides the text if non is provided
    public static void SetText(string text, string header = null)
    {
        s_instance._tooltipBody.gameObject.SetActive(!string.IsNullOrEmpty(text));
        s_instance._tooltipHeader.text = header;
        s_instance._tooltipText.text = text;
    }

    public void SetDimensions(Vector2 mousepos)
    {
            // Sets pivot position to prevent UI going off screen
            mousepos += mouseOffset;

            float pivotX = 0;
            float pivotY = 1;
            float RightX = (_tooltipBody.sizeDelta.x * _canvas.lossyScale.x) + mousepos.x;
            float BottomY = -((_canvas.sizeDelta.y/2 - _tooltipBody.sizeDelta.y) * _canvas.lossyScale.y);
            //Debug.Log("mouse pos " + mousepos.y + "| y bound: " + BottomY + " | diff" + (BottomY - mousepos.y));// +"| % over" + percentageOver + "| displace?:" + (percentageOver > 0));
            if(RightX > _canvas.lossyScale.x)
            {
                pivotX = 1;
            }
            if(BottomY > mousepos.y)
            {
                pivotY = 0;
            }
            _tooltipBody.pivot = new Vector2(pivotX,pivotY);
            transform.position = mousepos;
    }
}
