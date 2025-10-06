using UnityEngine;

public class Bed : MonoBehaviour
{
    [SerializeField] private GameObject _hiddenGirl;
    [SerializeField] private GameObject _pointer;

    private bool _isHidden;

    private void Awake()
    {
        _hiddenGirl.SetActive(false);
    }
    
    public void Hide()
    {
        _hiddenGirl.SetActive(true);
        Main.I.Girl.gameObject.SetActive(false);
        _isHidden = true;
        _pointer.SetActive(false);
    }

    public void Out()
    {
        _hiddenGirl.SetActive(false);
        Main.I.Girl.gameObject.SetActive(true);
        _isHidden = false;
        _pointer.SetActive(true);
    }

    private void Update()
    {
        if (_isHidden && Main.I.Game.GameState.CheckStates["MonsterHunting"] == "Disabled")
        {
            if (Input.GetKeyDown(KeyCode.Return)||Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.E))
                Out();
        }
    }
}
