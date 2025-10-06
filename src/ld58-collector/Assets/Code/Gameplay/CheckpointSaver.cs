using UnityEngine;

public class CheckpointSaver : MonoBehaviour
{
    public void Save()
    {
        Main.I.Game.SaveCheckPoint();
    }
}
