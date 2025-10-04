using System.Linq;
using UnityEngine;

public class Game
{
    private GameState _gameState;
    
    public GameState GameState => _gameState;

    private RoomView _currentRoomView;
    private RoomId _lastRoomId;

    public RoomView CurrentRoomView => _currentRoomView;

    public Game()
    {
        _gameState = new GameState();
    }

    public void Run()
    {
        LoadRoom(RoomId.ChildrenRoom);
        

    }

    public void LoadRoom(RoomId newRoom)
    {
        if (_currentRoomView != null)
        {
            _lastRoomId = _currentRoomView.RoomId;
            Object.Destroy(_currentRoomView.gameObject);
        }

        _currentRoomView = Resources.Load<RoomView>($"Rooms/{newRoom}");
        _currentRoomView = Object.Instantiate(_currentRoomView);

        if (_lastRoomId == RoomId.None)
        {
            Main.I.Girl.TeleportTo(_currentRoomView.GirlSpawnWp);
        }
        else
        {
            var doorView = _currentRoomView.DoorsViews.FirstOrDefault(dv => dv.NextRoom == _lastRoomId);
            if (doorView!=null)
                Main.I.Girl.TeleportTo(doorView.transform);
            else
                Main.I.Girl.TeleportTo(_currentRoomView.DoorsViews.First().transform);
        }
    }
}