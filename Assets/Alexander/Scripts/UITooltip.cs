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
    [SerializeField] private GameObject _tooltipBody;
    // reference to the text of the tooltip
    [SerializeField] private TextMeshProUGUI _tooltipText;
    // only one insyance can exist at a time
    private static UITooltip s_instance;

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
        transform.position = _canvas.transform.TransformPoint(pos);
    }

    // sets the text, or hides the text if non is provided
    public static void SetText(string text)
    {
        s_instance._tooltipBody.SetActive(!string.IsNullOrEmpty(text));
        s_instance._tooltipText.text = text;
    }
}
