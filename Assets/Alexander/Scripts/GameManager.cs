using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioManager _audio;
    public static GameManager Instance {get; private set;}
    public static bool GameActive {get; private set;}

    // Start is called before the first frame update
    void Awake()
    {
        if(Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
            SceneManager.sceneLoaded += OnSceneLoad;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public static void Play()
    {
        AudioManager.PlaySound(SoundKey.MenuConfirm, Vector2.zero);
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(1);
        GameActive = true;
    } 

    public static void MainMenu()
    {
        AudioManager.PlaySound(SoundKey.MenuExit, Vector2.zero);
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(0);
        GameActive = false;
    }


    public static void Credits()
    {
        AudioManager.PlaySound(SoundKey.MenuConfirm, Vector2.zero);
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(2);
        GameActive = false;
    }

    public static void WinGame()
    {
        GameActive = false;
        UIGameOver.TriggerGameOver();
    }

    public static void EndGame()
    {
        AudioManager.PlaySound(SoundKey.MenuExit, Vector2.zero);
        Application.Quit();
    }

    public void OnSceneLoad(Scene loadedScene, LoadSceneMode mode)
    {
        AudioManager.PlaySong(loadedScene.buildIndex);
        GameActive = loadedScene.buildIndex == 1;
    }
}
