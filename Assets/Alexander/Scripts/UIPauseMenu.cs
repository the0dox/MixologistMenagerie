using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPauseMenu : MonoBehaviour
{
    public static UIPauseMenu s_instance;
    [SerializeField] private GameObject _child;
    public static bool Paused {get; private set;}

    void Awake()
    {
        s_instance = this;
        UnPause();
    }

    public static void Pause()
    {
        Paused = true;
        Time.timeScale = 0;
        s_instance._child.SetActive(true);
    }

    public static void UnPause()
    {
        Paused = false;
        Time.timeScale = 1;
        s_instance._child.SetActive(false);
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Paused)
            {
                UnPause();
            }
            else
            {
                Pause();
            }
        }
    }

}
