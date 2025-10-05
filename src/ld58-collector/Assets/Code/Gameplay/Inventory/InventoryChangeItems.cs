using System.Collections.Generic;
using UnityEngine;

public class InventoryChangeItems : MonoBehaviour
{
    [SerializeField] private List<ItemId> _itemIds;
    [SerializeField] private bool _add;
    
    
    public void Run()
    {
        foreach (var itemId in _itemIds)
        {
            if (_add)
                Main.I.Game.AddItem(itemId);
            else
                Main.I.Game.RemoveItem(itemId);
        }
    }
}
