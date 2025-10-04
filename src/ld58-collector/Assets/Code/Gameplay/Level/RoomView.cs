using UnityEngine;

public class RoomView : MonoBehaviour
{
    [SerializeField] private RoomId _roomId;
    [SerializeField] private Transform _leftAnchor;
    [SerializeField] private Transform _rightAnchor;
    [SerializeField] private Transform _camera;
    [SerializeField] private Girl _girl;

    private Vector3 _camPos;

    private void Awake()
    {
        _camPos = _camera.transform.position;
    }
    private void LateUpdate()
    {
        _camPos.x = _girl.transform.position.x;
        _camera.transform.position = _camPos;
    }
}
