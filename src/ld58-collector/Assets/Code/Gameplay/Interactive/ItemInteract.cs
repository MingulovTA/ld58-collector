using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemInteract : MonoBehaviour, IInteractible
{
    [SerializeField] private List<ItemId> _itemIds;
    
    [SerializeField] private UnityEvent _onSuccess;
    [SerializeField] private UnityEvent _onFailed;
    [SerializeField] private GameObject _pointer;

    private void Awake()
    {
        if (_pointer!=null)
            _pointer.SetActive(false);
    }

    
    public void Intecact(PlayerInteractor playerInteractor)
    {
        foreach (var itemId in _itemIds)
        {
            if (!Main.I.Game.GameState.Inventory.Contains(itemId))
            {
                _onFailed?.Invoke();
                return;
            }
        }
        _onSuccess?.Invoke();
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
