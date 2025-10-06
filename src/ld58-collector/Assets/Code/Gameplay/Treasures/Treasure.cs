using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Treasure : MonoBehaviour
{
    [SerializeField] private TreasuresId _treasuresId;
    [SerializeField] private UnityEvent _onComplete;
    [SerializeField] private Vector3 _newScale = new Vector3(1,1,1);

    private Game _game;
    private bool _used;
    
    private void Awake()
    {
        _game = Main.I.Game;
    }

    public void Add()
    {
        if (_used) return;
        _used = true;
        Main.I.SoundService.PlaySfx("item_pick_up");
        transform.DOScale(_newScale, .5f);
        transform.DOMove(Camera.main.transform.position + Vector3.up * 5, .5f).OnComplete(delegate
        {
            _game.AddTreasure(_treasuresId);
            _onComplete?.Invoke();
        });
    }
}
