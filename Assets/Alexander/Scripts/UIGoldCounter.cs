using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// class that displays the amount of gold a player has
public class UIGoldCounter : MonoBehaviour
{
    private int _currentGold;
    private int _goldTarget;
    [SerializeField] private string _addition;
    private TextMeshProUGUI _text;
    [SerializeField] private AudioSource _source;

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
            if(_source)
            {
                _source.Stop();
            }
        }
        _text.text = _addition + " " + _currentGold;
    }
}
