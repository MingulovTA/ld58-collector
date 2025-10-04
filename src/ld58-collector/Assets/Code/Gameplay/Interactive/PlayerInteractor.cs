using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("OnTriggerEnter");
    }
    
    private void OnTriggerExit(Collider collider)
    {
        Debug.Log("OnTriggerExit");
    }
}
