using UnityEngine;

public class SfxPlayer : MonoBehaviour
{
    [SerializeField] private string _sfxId;

    public void Play()
    {
        Main.I.SoundService.PlaySfx(_sfxId);
    }
}
