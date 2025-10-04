using UnityEngine;

public class GirlStateView : MonoBehaviour
{
    [SerializeField] private GirlStateId _girlStateId;

    public GirlStateId GirlStateId => _girlStateId;
    
    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
