using System.Collections.Generic;

public class GameState
{
    public float PlayerX;
    public float PlayerY;

    public RoomId CurrentRoomId;

    public List<TreasuresId> TreasuresIds = new List<TreasuresId>();

    public Dictionary<string, string> CheckStates = new Dictionary<string, string>
    {
        {"Bear", "InSitu"},
        {"Picture", "InSitu"},
        
        {"Needle", "InSitu"},
        {"Thread", "InSitu"},
        {"ParentSkaf", "InSitu"},
        {"ParentSkatulka", "InSitu"},
        {"Book", "InSitu"},
        
    };

    public List<ItemId> Inventory = new List<ItemId>();
}