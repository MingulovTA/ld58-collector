using System.Collections.Generic;
using UnityEngine;

public class InventoryUiBar : MonoBehaviour
{
    [SerializeField] private List<InventoryUi> _itemUis;

    private Game _game;

    private void Awake()
    {
        _game = Main.I.Game;
    }

    private void OnEnable()
    {
        _game.OnGameStateUpdate += UpdateView;
        UpdateView();
    }

    private void OnDisable()
    {
        _game.OnGameStateUpdate += UpdateView;
    }

    private void UpdateView()
    {
        foreach (var itemUi in _itemUis)
        {
            itemUi.gameObject.SetActive(_game.GameState.Inventory.Contains(itemUi.ItemId));
        }
    }
}
