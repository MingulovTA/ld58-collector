using UnityEngine;
using UnityEngine.Events;

public class SimpleInteraction : MonoBehaviour, IInteractible
{
    [SerializeField] private UnityEvent _onInteract;
    [SerializeField] private GameObject _pointer;

    private void Awake()
    {
        if (_pointer!=null)
            _pointer.SetActive(false);
    }
    public void Intecact(PlayerInteractor playerInteractor)
    {
        _onInteract?.Invoke();
    }

    public void Enable()
    {
        if (_pointer!=null)
            _pointer.SetActive(true);
    }

    public void Disable()
    {
        if (_pointer!=null)
            _pointer.SetActive(false);
    }
}
