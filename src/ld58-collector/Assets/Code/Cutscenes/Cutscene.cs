using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Cutscene : MonoBehaviour
{
    [SerializeField] private List<CutscenePage> _pages;
    
    [SerializeField] private UnityEvent _onComplete;
    private int _index;
    
    void Start()
    {
        foreach (var cutscenePage in _pages)
            cutscenePage.gameObject.SetActive(false);
        NextOrComplete();
    }

    private void NextOrComplete()
    {
        foreach (var cutscenePage in _pages)
            cutscenePage.gameObject.SetActive(false);
        
        if (_index < _pages.Count)
        {
            _pages[_index].Play(NextOrComplete);
            _index++;
        }
        else
        {
            _onComplete?.Invoke();
            _pages.Last().gameObject.SetActive(true);
        }
    }
}
