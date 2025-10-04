using System;
using UnityEngine;

public class Girl : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private CharacterController _characterController;   
    [SerializeField] private GirlView _girlView;
    
    private int _direction = 1;
    private Vector3 _velocity;
    
    private void Awake()
    {
        _girlView.SetState(GirlStateId.Idle);
    }

    private void Update()
    {
        Debug.Log(Input.GetAxis("Horizontal"));
        var inputX = Input.GetAxis("Horizontal");

        if (Math.Abs(inputX) > 0.01f)
        {
            Move(inputX);
        }
        else
        {
            Stop();
        }

        _velocity.y = -10;
        _characterController.Move(_velocity*Time.deltaTime);
    }

    private void Move(float dx)
    {
        _girlView.SetState(GirlStateId.Walk);
        if (dx>0)
            _girlView.SetDirection(1);
        else
            _girlView.SetDirection(-1);
        _velocity.x = dx * _speed;
    }

    private void Stop()
    {
        _girlView.SetState(GirlStateId.Idle);
    }
}
