using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _angularSpeed = 2;
    [SerializeField] private float _radiusIncrease = 0.1f;
    [SerializeField] private int _direction = 1;
    [SerializeField] private int _reward = 100;

    private float _radius;
    private Vector3 _centerPosition;
    private float _currentAngle;
    private Vector3 _lastPosition;
    private Action<int> OnDeath;
   
    public int Reward => _reward;

    public void Initialize(Vector3 centerPosition, float initialRadius, float currentAngle, Action<int> onDeathCallback)
    {
        _centerPosition = centerPosition;
        _currentAngle = currentAngle;
        _radius = initialRadius;
        OnDeath = onDeathCallback;

        UpdatePosition();
    }

    private void FixedUpdate()
    {
        UpdatePosition();

        // Rotate ship to look at direction of movement
        transform.LookAt(transform.position - _lastPosition);

        _currentAngle += _direction * _angularSpeed * Time.fixedDeltaTime;
        _radius += _radiusIncrease * Time.fixedDeltaTime;
        _lastPosition = transform.position;
    }

    private void UpdatePosition()
    {
        float x = _centerPosition.x + Mathf.Cos(_currentAngle) * _radius;
        float z = _centerPosition.z + Mathf.Sin(_currentAngle) * _radius;

        transform.position = new Vector3(x, 0, z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Bullet>())
        {
            DestroyShip();
        }
    }

    public void DestroyShip()
    {
        OnDeath?.Invoke(_reward);
        Destroy(gameObject);
    }
}