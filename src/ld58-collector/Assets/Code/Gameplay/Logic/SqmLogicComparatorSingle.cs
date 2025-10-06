using UnityEngine;
using UnityEngine.Events;

public class SqmLogicComparatorSingle : MonoBehaviour
{
    [SerializeField] private string _objectId;
    [SerializeField] private string _stateId;

    [SerializeField] private UnityEvent _onSuccess;
    [SerializeField] private UnityEvent _onFailed;
    public void TryToRun()
    {
        if (Main.I.Game.GameState.CheckStates[_objectId]==_stateId)
            _onSuccess?.Invoke();
        else
            _onFailed?.Invoke();
    }
}
