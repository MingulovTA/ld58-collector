using System.Collections;
using DG.Tweening;
using UnityEngine;

public class DoorView : MonoBehaviour, IInteractible
{
    [SerializeField] private RoomId _nextRoom;
    [SerializeField] private GameObject _opened;
    [SerializeField] private GameObject _closed;

    public RoomId NextRoom => _nextRoom;

    private InputService _inputService;
    private Game _game;
    private Girl _girl;
    private FadeScreenService _fadeScreenService;

    private void Awake()
    {
        _inputService = Main.I.InputService;
        _game = Main.I.Game;
        _fadeScreenService = Main.I.FadeScreenService;
        _girl = Main.I.Girl;
    }

    public void Intecact(PlayerInteractor playerInteractor)
    {
        StartCoroutine(Enter());
    }

    private IEnumerator Enter()
    {
        OpenAnim();
        _inputService.AddLocker(this);
        _girl.transform.DOMoveX(transform.position.x, .5f);
        yield return _fadeScreenService.FadeIn(.5f);
        _inputService.RemoveLocker(this);
        _game.LoadRoom(_nextRoom);
    }

    public void CloseAnim()
    {
        _opened.SetActive(false);
        _closed.SetActive(true);
    }

    public void OpenAnim()
    {
        _opened.SetActive(true);
        _closed.SetActive(false);
    }
}

public interface IInteractible
{
    void Intecact(PlayerInteractor playerInteractor);
}
