using System.Collections.Generic;

public class GameState
{
    public float PlayerX;
    public float PlayerY;
    public RoomId CurrentRoomId;
    public List<TreasuresId> TreasuresIds = new List<TreasuresId>();
    public List<ItemId> Inventory = new List<ItemId>();

    public Dictionary<string, string> CheckStates = new Dictionary<string, string>
    {
        {"Needle", "InSitu"},
        {"Thread", "InSitu"},
        {"ParentSkaf", "InSitu"},
        {"ParentSkatulka", "InSitu"},
    };
}