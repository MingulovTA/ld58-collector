using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundService : MonoBehaviour
{
    [SerializeField] private List<AudioSource> _musics;
    [SerializeField] private List<AudioSource> _sfxes;

    private string _current;
    
    public void PlayMusic(string key)
    {
        Debug.Log("PlayMusic "+key);
        if (_current==key) return;
        foreach (var audioSource in _musics)
            audioSource.Stop();
        _current = key;
        _musics.First(ass => ass.gameObject.name == key).Play();
    }
    
    public void StopMusic()
    {
        foreach (var audioSource in _musics)
            audioSource.Stop();
    }
    
    public void PlaySfx(string key)
    {
        _sfxes.First(ass => ass.gameObject.name == key).Play();
    }
}
