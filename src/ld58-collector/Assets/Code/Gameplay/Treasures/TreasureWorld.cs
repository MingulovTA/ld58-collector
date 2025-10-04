using UnityEngine;
using UnityEngine.Events;

public class TreasureWorld : MonoBehaviour, IInteractible
{
    [SerializeField] private TreasuresId _treasuresId;
    [SerializeField] private UnityEvent _onPickup;

    private Game _game;
    
    private void Awake()
    {
        _game = Main.I.Game;
        if (_game.GameState.TreasuresIds.Contains(_treasuresId))
            gameObject.SetActive(false);
    }

    public void Intecact(PlayerInteractor playerInteractor)
    {
        playerInteractor.Disable();
        _game.AddTreasure(_treasuresId);
        _onPickup?.Invoke();
        gameObject.SetActive(false);
    }
}
