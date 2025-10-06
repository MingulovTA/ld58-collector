using UnityEngine;
using UnityEngine.Events;

public class DialogMessage : MonoBehaviour
{
    [SerializeField] private string _title;
    [SerializeField] private string _message;
    [SerializeField] private DialogActorId _dialogActorId;
    [SerializeField] private UnityEvent _onComplete;

    private DialogService _dialogService;
    private InputService _inputService;
    
    private void Awake()
    {
        _dialogService = Main.I.DialogService;
        _inputService = Main.I.InputService;
    }
    
    public void Say()
    {
        Debug.Log("say");
        _inputService.AddLocker(this);
        _dialogService.Show(_message,_title, _dialogActorId, OnComplete);
    }

    private void OnComplete()
    {
        _inputService.RemoveLocker(this);
        _onComplete?.Invoke();
    }
}
