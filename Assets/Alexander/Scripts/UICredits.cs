using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICredits : MonoBehaviour
{
    [SerializeField] private GameObject child;
    private UICredits s_instance;
    public void Awake()
    {
        s_instance = this;
        ToggleCredits(false);
    }

    public void ToggleCredits(bool value)
    {
        child.SetActive(value);
    }
}
