using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GirlView : MonoBehaviour
{
    [SerializeField] private List<GirlStateView> _statesViews;
    [SerializeField] private int _direction = 1;
    [SerializeField] private Animator _animator;
    
    private GirlStateView _girlStateView;
    private GirlStateId _currentState = GirlStateId.None;
    
    public void SetState(GirlStateId girlStateId)
    {
        _animator.SetBool("b_walk",_currentState==GirlStateId.Walk);
        if (_currentState==girlStateId) return;
        
        //if (_girlStateView != null)
        //    _girlStateView.Disable();
        //_girlStateView = _statesViews.First(sv => sv.GirlStateId == girlStateId);
        //_girlStateView.Enable();
        
        _currentState = girlStateId;
    }

    public void SetDirection(int newDirection)
    {
        if (_direction==newDirection) return;
        _direction = newDirection;
        transform.localScale = new Vector3(_direction,1,1);
    }
}
