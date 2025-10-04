using UnityEngine;
using UnityEngine.Events;

public class SimpleInteraction : MonoBehaviour, IInteractible
{
    [SerializeField] private UnityEvent _onInteract;
    public void Intecact(PlayerInteractor playerInteractor)
    {
        _onInteract?.Invoke();
    }
}
