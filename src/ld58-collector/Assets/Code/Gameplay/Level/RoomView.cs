using UnityEngine;

using UnityEngine;

public class RoomView : MonoBehaviour
{
    [SerializeField] private RoomId _roomId;
    [SerializeField] private Transform _leftAnchor;
    [SerializeField] private Transform _rightAnchor;
    [SerializeField] private Camera _camera;
    [SerializeField] private Girl _girl;

    private Vector3 _camPos;
    private float _halfWidth;

    private void Awake()
    {
        _camPos = _camera.transform.position;
        UpdateCameraHalfWidth();
    }

    private void LateUpdate()
    {
        // Следим за изменением соотношения сторон (если игра может растягиваться)
        UpdateCameraHalfWidth();

        // Двигаем камеру за игроком
        _camPos.x = _girl.transform.position.x;

        // Ограничиваем по анчорам
        float leftLimit = _leftAnchor.position.x + _halfWidth;
        float rightLimit = _rightAnchor.position.x - _halfWidth;
        _camPos.x = Mathf.Clamp(_camPos.x, leftLimit, rightLimit);

        // Применяем позицию
        _camera.transform.position = _camPos;
    }

    private void UpdateCameraHalfWidth()
    {
        // orthographicSize — половина высоты камеры
        // а половина ширины зависит от текущего соотношения сторон экрана
        _halfWidth = _camera.orthographicSize * _camera.aspect;
    }
}
