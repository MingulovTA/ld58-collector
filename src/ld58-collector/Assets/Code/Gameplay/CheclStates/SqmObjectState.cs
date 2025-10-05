using System.Linq;
using UnityEngine;

public class SqmObjectState : MonoBehaviour
{
    [SerializeField] private string _objectId;
    [SerializeField] private string _state;
    [SerializeField] private bool _isDefaultState;

    private Game _game;

    private void Awake()
    {
        _game = Main.I.Game;
    }
    public void Init()
    {
        if (!_game.GameState.CheckStates.ContainsKey(_objectId))
            _game.GameState.CheckStates.Add(_objectId,_state);
        _game.OnGameStateUpdate += UpdateView;
    }

    public void Dispose()
    {
        _game.OnGameStateUpdate -= UpdateView;
    }

    private void UpdateView()
    {
        
    }
}
