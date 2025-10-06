using System.Collections.Generic;

public class GameState
{
    public float PlayerX;
    public float PlayerY;

    public RoomId CurrentRoomId;

    public List<TreasuresId> TreasuresIds = new List<TreasuresId>();

    public Dictionary<string, string> CheckStates = new Dictionary<string, string>
    {
        {"MonsterHunting", "Disabled"},
        
        {"Bear", "InSitu"},
        {"Picture", "InSitu"},
        {"Chair_ChildRoom", "InSitu"}, //Заменить на InSitu
        {"Chair_Kichen", "Stolen"},
        
        {"Needle", "InSitu"},
        {"Thread", "InSitu"},
        {"ParentSkaf", "InSitu"},
        {"ParentSkatulka", "InSitu"},
        {"Book", "InSitu"},
        {"Dr_Parent_Living", "Locked"},
        
        
        {"Gramophone", "InSitu"}, //Playing
        {"Dr_Living_Kitchen", "Locked"},
        {"Vinyl", "InSitu"},
        {"Shelf", "InSitu"},
        {"Flower", "InSitu"},
        
    };

    public List<ItemId> Inventory = new List<ItemId>();
}