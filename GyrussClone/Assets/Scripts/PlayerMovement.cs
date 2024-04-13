using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _angularSpeed = 2;
    [SerializeField] private float _radius = 10;

    private Transform _targetTransform;
    private float _currentAngle = 0;
    private float _direction = 0;

    public void Initialize(Transform target)
    {
        _targetTransform = target;
    }

    void FixedUpdate()
    {
        float x = _targetTransform.position.x + Mathf.Cos(_currentAngle) * _radius;
        float z = _targetTransform.position.z + Mathf.Sin(_currentAngle) * _radius;

        transform.position = new Vector3(x, 0, z);

        // Rotate ship to look at center
        transform.LookAt(_targetTransform.position);

        _currentAngle += _direction * _angularSpeed * Time.fixedDeltaTime;
    }


    public void OnMovement(InputAction.CallbackContext context)
    {
        _direction = context.ReadValue<float>();
    }
}
