using System.Collections.Generic;
using UnityEngine;

public class TreasureUiBar : MonoBehaviour
{
    [SerializeField] private List<TreasureUi> _treasureUis;

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
        foreach (var treasureUi in _treasureUis)
        {
            if (_game.GameState.TreasuresIds.Contains(treasureUi.TreasuresId))
                treasureUi.Enable();
            else
                treasureUi.Disable();
        }
    }
}
