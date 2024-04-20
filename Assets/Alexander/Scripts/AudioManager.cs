using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private Dictionary<SoundKey, AudioClip> _sounds;
    public static AudioManager s_instance;
    [SerializeField] private AudioSource _songSource;
    [SerializeField] private SoundBinding[] _soundInitalizer;

    // Start is called before the first frame update
    void Start()
    {
        s_instance = this;
        _sounds = new Dictionary<SoundKey, AudioClip>();
        foreach(SoundBinding binding in _soundInitalizer)
        {
            _sounds.Add(binding.key, binding.clip);
        }
    }

    public static void PlaySound(SoundKey key, Vector2 position)
    {
        if(!s_instance._sounds.ContainsKey(key))
        {
            Debug.LogWarning("No binding for " + key);
        }
        else
        {
            AudioSource.PlayClipAtPoint(s_instance._sounds[key], position);
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
    Ingredient,
    Potion,
    Talk,
    Happy,
    Sad,
    Coin,
    Leave,
}