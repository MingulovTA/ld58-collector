using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class RoomView : MonoBehaviour
{
    [SerializeField] private RoomId _roomId;
    [SerializeField] private Transform _leftAnchor;
    [SerializeField] private Transform _rightAnchor;
    [SerializeField] private Transform _girlSpawnWp;
    [SerializeField] private List<DoorView> _doorsViews;
    [SerializeField] private List<SqmObjectState> _sqmObjectStates;
    [SerializeField] private bool _isCameraFollow = true;
    private Girl _girl;

    private Vector3 _camPos;
    private float _halfWidth;

    public RoomId RoomId => _roomId;
    public Transform GirlSpawnWp => _girlSpawnWp;

    public List<DoorView> DoorsViews => _doorsViews;

    private void Awake()
    {
        _girl = Main.I.Girl;
        _camPos = Camera.main.transform.position;
        UpdateCameraHalfWidth();
    }

    private void LateUpdate()
    {
        if (!_isCameraFollow)
        {
            _camPos.x = 0;
            Camera.main.transform.position = _camPos;
            return;
        }
        UpdateCameraHalfWidth();

        _camPos.x = _girl.transform.position.x;

        float leftLimit = _leftAnchor.position.x + _halfWidth;
        float rightLimit = _rightAnchor.position.x - _halfWidth;
        _camPos.x = Mathf.Clamp(_camPos.x, leftLimit, rightLimit);

        Camera.main.transform.position = _camPos;
    }

    private void UpdateCameraHalfWidth()
    {
        _halfWidth = Camera.main.orthographicSize * Camera.main.aspect;
    }

    private void OnValidate()
    {
        _doorsViews = GetComponentsInChildren<DoorView>().ToList();
        _sqmObjectStates = GetComponentsInChildren<SqmObjectState>().ToList();
    }

    private void OnEnable()
    {
        foreach (var sqmObjectState in _sqmObjectStates)
            sqmObjectState.Init();
    }

    private void OnDisable()
    {
        foreach (var sqmObjectState in _sqmObjectStates)
            sqmObjectState.Dispose();
    }
}
