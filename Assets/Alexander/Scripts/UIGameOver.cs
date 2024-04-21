using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    private static UIGameOver s_instance;
    [SerializeField]private GameObject _child;
    // Start is called before the first frame update
    void Start()
    {
        s_instance = this;
        _child.SetActive(false);
    }

    public static void TriggerGameOver()
    {
        s_instance._child.SetActive(true);
    }
}
