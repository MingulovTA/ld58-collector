using System.Collections;
using System.Linq;
using UnityEngine;

public class Game
{
    private GameState _gameState;
    private ICoroutineRunner _coroutineRunner;
    
    public GameState GameState => _gameState;

    private RoomView _currentRoomView;
    private RoomId _lastRoomId;

    public RoomView CurrentRoomView => _currentRoomView;

    public Game(ICoroutineRunner coroutineRunner)
    {
        _gameState = new GameState();
        _coroutineRunner = coroutineRunner;
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

        DoorView doorView = null;
        if (_lastRoomId == RoomId.None)
        {
            Main.I.Girl.TeleportTo(_currentRoomView.GirlSpawnWp);
        }
        else
        {
            doorView = _currentRoomView.DoorsViews.FirstOrDefault(dv => dv.NextRoom == _lastRoomId);
            if (doorView!=null)
                Main.I.Girl.TeleportTo(doorView.transform);
            else
                Main.I.Girl.TeleportTo(_currentRoomView.DoorsViews.First().transform);
        }

        _coroutineRunner.Run(FadeOut(doorView));
    }

    private IEnumerator FadeOut(DoorView doorView)
    {
        if (doorView!=null)
            doorView.OpenAnim();
        Main.I.InputService.AddLocker(this);
        yield return Main.I.FadeScreenService.FadeOut(.5f);
        Main.I.InputService.RemoveLocker(this);
        if (doorView!=null)
            doorView.CloseAnim();
    }
}