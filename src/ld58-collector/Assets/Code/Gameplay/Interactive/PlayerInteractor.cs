using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    private IInteractible _target;
    private InputService _inputService;

    private void Awake()
    {
        _inputService = Main.I.InputService;
    }
    private void OnTriggerEnter(Collider collider)
    {
        var target = collider.GetComponent<IInteractible>();
        if (target != null)
            _target = target;
    }
    
    private void OnTriggerExit(Collider collider)
    {
        var target = collider.GetComponent<IInteractible>();
        if (target != null)
            _target = null;
    }

    private void Update()
    {
        if (_inputService.GetKeyDown(KeyCode.Return) || _inputService.GetKeyDown(KeyCode.E) || _inputService.GetKeyDown(KeyCode.Space))
        {
            if (_target != null)
                _target.Intecact(this);
        }
    }
}
