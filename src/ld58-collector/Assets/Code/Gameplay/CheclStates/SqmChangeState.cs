using UnityEngine;

public class SqmChangeState : MonoBehaviour
{
    [SerializeField] private string _objectId;
    [SerializeField] private string _newState;

    public void Run()
    {
        Main.I.Game.ChangeCheckpoint(_objectId, _newState);
    }
}
