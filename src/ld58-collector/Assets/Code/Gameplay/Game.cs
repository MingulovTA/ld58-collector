using System;
using System.Collections;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class Game
{
    private GameState _gameState;
    private ICoroutineRunner _coroutineRunner;
    private RoomView _currentRoomView;
    private RoomId _lastRoomId;


    public GameState GameState => _gameState;
    public RoomView CurrentRoomView => _currentRoomView;

    public bool IsCollectedAllTreasures => _gameState.TreasuresIds.Contains(TreasuresId.Doll) &&
                                           _gameState.TreasuresIds.Contains(TreasuresId.Flower) &&
                                           _gameState.TreasuresIds.Contains(TreasuresId.Pendant) &&
                                           _gameState.TreasuresIds.Contains(TreasuresId.Picture);

    public event Action OnGameStateUpdate;

    public Game(ICoroutineRunner coroutineRunner)
    {
        _gameState = new GameState();
        _coroutineRunner = coroutineRunner;
    }

    public void Run()
    {
        Main.I.Monster.gameObject.SetActive(false);
        LoadRoom(RoomId.ChildrenRoom);
        SaveCheckPoint();
    }

    public void LoadRoom(RoomId newRoom)
    {
        Main.I.MonsterAttackRunner.StopAttackingIfNeed();
        if (_currentRoomView != null)
        {
            _lastRoomId = _currentRoomView.RoomId;
            Object.Destroy(_currentRoomView.gameObject);
        }

        _currentRoomView = Resources.Load<RoomView>($"Rooms/{newRoom}");
        _currentRoomView = Object.Instantiate(_currentRoomView);
        _gameState.CurrentRoomId = _currentRoomView.RoomId;
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
        Main.I.MonsterAttackRunner.TryToRun();
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

    public void AddTreasure(TreasuresId treasuresId)
    {
        _gameState.TreasuresIds.Add(treasuresId);
        OnGameStateUpdate?.Invoke();
    }

    public void AddItem(ItemId itemId)
    {
        if (!_gameState.Inventory.Contains(itemId))
        {
            _gameState.Inventory.Add(itemId);
            OnGameStateUpdate?.Invoke();
        }
    }

    public void RemoveItem(ItemId itemId)
    {
        if (_gameState.Inventory.Contains(itemId))
        {
            _gameState.Inventory.Remove(itemId);
            OnGameStateUpdate?.Invoke();
        }
    }

    public void ChangeCheckpoint(string objectId, string newState)
    {
        if (_gameState.CheckStates[objectId] != newState)
        {
            _gameState.CheckStates[objectId] = newState;
            OnGameStateUpdate?.Invoke();
        }
    }

    public void Complete()
    {
        Main.I.gameObject.SetActive(false);
        SceneManager.LoadScene(2);
    }

    private string _checkPointStr;
    public void LoadCheckpoint()
    {
        _gameState = JsonConvert.DeserializeObject<GameState>(_checkPointStr);
        LoadRoom(_gameState.CurrentRoomId);
        Main.I.Girl.TeleportToCheckPoint();
        OnGameStateUpdate?.Invoke();
    }

    public void SaveCheckPoint()
    {
        _gameState.PlayerX = Main.I.Girl.transform.position.x;
        _gameState.PlayerY = Main.I.Girl.transform.position.y;
        _checkPointStr = JsonConvert.SerializeObject(_gameState);
    }
}