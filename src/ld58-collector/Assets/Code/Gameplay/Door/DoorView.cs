using UnityEngine;

public class DoorView : MonoBehaviour, IInteractible
{
    [SerializeField] private RoomId _nextRoom;
    [SerializeField] private GameObject _opened;
    [SerializeField] private GameObject _closed;

    public RoomId NextRoom => _nextRoom;

    public void Intecact()
    {
        Main.I.Game.LoadRoom(_nextRoom);
    }
}

public interface IInteractible
{
    void Intecact();
}
