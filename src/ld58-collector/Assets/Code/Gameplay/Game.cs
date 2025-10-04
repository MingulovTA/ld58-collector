using UnityEngine;

public class Game
{
    private GameState _gameState;
    
    public GameState GameState => _gameState;

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
        
    }
}