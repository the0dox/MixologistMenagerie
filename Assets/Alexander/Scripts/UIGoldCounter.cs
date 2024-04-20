using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// class that displays the amount of gold a player has
public class UIGoldCounter : MonoBehaviour
{
    private int _currentGold;
    private int _goldTarget;
    private TextMeshProUGUI _text;

    void Start()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnGoldUpdated(int newGold)
    {
        _goldTarget = newGold;
    }

    void FixedUpdate()
    {
        if(_currentGold < _goldTarget)
        {
            _currentGold = Mathf.CeilToInt(Mathf.Lerp(_currentGold, _goldTarget, Time.fixedDeltaTime));
        }
        else
        {
            _currentGold = _goldTarget;
        }
        _text.text = "Gold: " + _currentGold;
    }
}
