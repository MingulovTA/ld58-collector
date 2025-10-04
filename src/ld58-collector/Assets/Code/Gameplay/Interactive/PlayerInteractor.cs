using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    private IInteractible _target;
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
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
        {
            if (_target != null)
            {
                _target.Intecact();
            }
        }
    }
}
