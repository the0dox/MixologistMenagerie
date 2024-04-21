using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private Dictionary<SoundKey, AudioClip> _sounds;
    public static AudioManager s_instance;
    [SerializeField] private AudioSource _songSource;
    [SerializeField] private AudioSource _fadeInSource;
    [SerializeField] private SoundBinding[] _soundInitalizer;
    [SerializeField] private AudioClip _introSong;
    [SerializeField] private AudioClip _mainSong;
    [SerializeField] private AudioClip _fullSong;
    [SerializeField] private float cutOffPercentage = 0.75f;
    private bool _fade;

    // Start is called before the first frame update
    void Awake()
    {
        if(s_instance == null)
        {
            s_instance = this;
            _sounds = new Dictionary<SoundKey, AudioClip>();
            foreach(SoundBinding binding in _soundInitalizer)
            {
                _sounds.Add(binding.key, binding.clip);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public static void PlaySong(int index)
    {
        Debug.Log("playing song" + index);
        s_instance._songSource.volume = 1;
        s_instance._fadeInSource.volume = 0;
        if(index == 1)
        {
            s_instance._fade = true;
            s_instance._songSource.clip = s_instance._mainSong;
        }
        else
        {
            s_instance._songSource.clip = s_instance._introSong;
        }
        s_instance._songSource.time = 0;
        s_instance._songSource.Play();
    }

    public static void PlaySound(SoundKey key, Vector2 position)
    {
        if(!s_instance._sounds.ContainsKey(key))
        {
            Debug.LogWarning("No binding for " + key);
        }
        else
        {
            AudioSource.PlayClipAtPoint(s_instance._sounds[key], Vector2.zero, 0.8f);
        }
    }

    public void Update()
    {
        if(_fade)
        {
            float songProgress = _songSource.time/_introSong.length;
            if(songProgress > cutOffPercentage)
            {
                _songSource.volume = Mathf.Lerp(0,1, (songProgress - cutOffPercentage) / 1 - cutOffPercentage);
                _fadeInSource.volume = Mathf.Lerp(1,0, (songProgress - cutOffPercentage) / 1 - cutOffPercentage);
            }
            if(songProgress > 0.99f)
            {
                _fadeInSource.volume = 1;
                _songSource.volume = 0;
                _fade = false;
            }
        }
    }
}

[System.Serializable]
public struct SoundBinding
{
    public SoundKey key;
    public AudioClip clip;
}

public enum SoundKey
{
    None,
    Blend,
    Pickup,
    DropSuccessful,
    DropFailure,
    Potion,
    Talk,
    Happy,
    Sad,
    Coin,
    Leave,
    TalkA,
    TalkB,
    TalkC,
    TalkD,
    PotionReady1,
    PotionReady2,
    MenuConfirm,
    MenuHover,
    MenuExit,
    WalkUp,
    WalkAway
}