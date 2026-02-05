using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float cycleTime = 5f;
    private float _currentTime = 0f;
    private float _speed = 1f;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    void Update()
    {
        _currentTime += _speed * Time.deltaTime;
        if (_currentTime > cycleTime){ _speed = -1f; }
        if (_currentTime < 0){ _speed = 1f; }
        transform.position = Vector3.Lerp(pointA.position, pointB.position, _currentTime/cycleTime);
    }
}
