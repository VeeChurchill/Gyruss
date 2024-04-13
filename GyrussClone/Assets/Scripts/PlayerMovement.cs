using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _angularSpeed = 2;
        [SerializeField] private float _radius = 10;

        private float _currentAngle;
        private float _direction;

        private Transform _targetTransform;

        public void Initialize(Transform target)
        {
            _targetTransform = target;
        }

        private void FixedUpdate()
        {
            var x = _targetTransform.position.x + Mathf.Cos(_currentAngle) * _radius;
            var z = _targetTransform.position.z + Mathf.Sin(_currentAngle) * _radius;

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
}