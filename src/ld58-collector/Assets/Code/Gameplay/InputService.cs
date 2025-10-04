using System.Collections.Generic;
using UnityEngine;

public class InputService
{

    private List<object> _lockers = new List<object>();

    private bool _isLocked;

    public void AddLocker(object locker)
    {
        if (!_lockers.Contains(locker))
            _lockers.Add(locker);
        _isLocked = _lockers.Count != 0;
    }

    public void RemoveLocker(object locker)
    {
        if (_lockers.Contains(locker))
            _lockers.Remove(locker);
        _isLocked = _lockers.Count != 0;
    }

    public float GetAxis(string axisId)
    {
        return _isLocked ? 0 : Input.GetAxis(axisId);
    }

    public bool GetKeyDown(KeyCode key)
    {
        return _isLocked ? false : Input.GetKeyDown(key);
    }
}
