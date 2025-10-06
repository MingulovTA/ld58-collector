using System.Collections.Generic;
using UnityEngine;

public class SoundsDeployer : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _clips;
    
    [ContextMenu("DEPLOY")]
    private void Deploy()
    {
        foreach (var clip in _clips)
        {
            GameObject gm = new GameObject();
            gm.transform.SetParent(transform);
            gm.name = clip.name;
            var audioSource = gm.AddComponent<AudioSource>();
            audioSource.clip = clip;
            if (clip.name.ToLower().Contains("loop"))
                audioSource.loop = true;
            audioSource.playOnAwake = false;
        }
    }
}
