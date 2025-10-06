using System.Collections;
using DG.Tweening;
using UnityEngine;

public class DoorView : MonoBehaviour, IInteractible
{
    [SerializeField] private RoomId _nextRoom;
    [SerializeField] private GameObject _opened;
    [SerializeField] private GameObject _closed;
    [SerializeField] private GameObject _pointer;
    [SerializeField] private bool _isMonsterDoor;

    public RoomId NextRoom => _nextRoom;
    public bool IsMonsterDoor => _isMonsterDoor;

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
        if (_pointer!=null)
            _pointer.SetActive(false);
    }

    public void Intecact(PlayerInteractor playerInteractor)
    {
        StartCoroutine(Enter());
    }

    public void Enable()
    {
        if (_pointer!=null)
            _pointer.SetActive(true);
    }

    public void Disable()
    {
        if (_pointer!=null)
            _pointer.SetActive(false);
    }

    private IEnumerator Enter()
    {
        Main.I.SoundService.PlaySfx("wood_door");
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
    
    public bool IsActive()
    {
        return gameObject.activeSelf && gameObject.activeInHierarchy;
    }
}