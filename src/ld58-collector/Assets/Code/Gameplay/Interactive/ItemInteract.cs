using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemInteract : MonoBehaviour
{
    [SerializeField] private List<ItemId> _itemIds;
    
    [SerializeField] private UnityEvent _onSuccess;
    [SerializeField] private UnityEvent _onFailed;

    
    public void Intecact(PlayerInteractor playerInteractor)
    {
        foreach (var itemId in _itemIds)
        {
            if (!Main.I.Game.GameState.Inventory.Contains(itemId))
            {
                _onFailed?.Invoke();
            }
        }
        _onSuccess?.Invoke();
    }
}
