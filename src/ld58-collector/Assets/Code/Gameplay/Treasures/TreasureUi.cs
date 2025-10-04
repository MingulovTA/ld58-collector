using UnityEngine;
using UnityEngine.UI;

public class TreasureUi : MonoBehaviour
{
    [SerializeField] private TreasuresId _treasuresId;
    [SerializeField] private GameObject _icon;

    public TreasuresId TreasuresId => _treasuresId;

    public void Enable()
    {
        _icon.gameObject.SetActive(true);
    }

    public void Disable()
    {
        _icon.gameObject.SetActive(false);
    }
}
