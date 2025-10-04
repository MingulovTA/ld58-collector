using System;
using UnityEngine;

public class Girl : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    [SerializeField] private GirlView _girlView;
    
    private int _direction = 1;
    private Vector3 _pos;
    
    private void Awake()
    {
        _pos = transform.position;
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
    }

    private void Move(float dx)
    {
        _girlView.SetState(GirlStateId.Walk);
        if (dx>0)
            _girlView.SetDirection(1);
        else
            _girlView.SetDirection(-1);
        
        _pos.x += dx * _speed * Time.deltaTime;;
        transform.position = _pos;
    }

    private void Stop()
    {
        _girlView.SetState(GirlStateId.Idle);
    }
}
