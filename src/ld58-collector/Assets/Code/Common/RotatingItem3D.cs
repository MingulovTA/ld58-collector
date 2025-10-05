using UnityEngine;

public class RotatingItem3D : MonoBehaviour
{
    [SerializeField] private Vector3 _offset = Vector3.zero;
    private Transform _transform;
    private Vector3 _angle;

    private void Awake()
    {
        _transform = transform;
        _angle = _transform.localEulerAngles;
    }


    private void Update()
    {
        _angle+=_offset*Time.deltaTime;
        _transform.localEulerAngles = _angle;
    }
}
