using System.Collections.Generic;
using UnityEngine;

public class InputService
{

    private List<Object> _lockers = new List<Object>();

    private bool _isLocked;

    public void AddLocker(Object locker)
    {
        if (!_lockers.Contains(locker))
            _lockers.Add(locker);
        _isLocked = _lockers.Count != 0;
    }

    public void RemoveLocker(Object locker)
    {
        if (_lockers.Contains(locker))
            _lockers.Remove(locker);
        _isLocked = _lockers.Count != 0;
    }

    public float GetAxis(string axisId)
    {
        return _isLocked ? 0 : Input.GetAxis(axisId);
    }
}
