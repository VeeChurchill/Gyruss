using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private readonly float _angleOfMovement = 100f;
        [SerializeField] private readonly float _distanceDespawn = 20f;
        [SerializeField] private readonly float _maxDistance = 10f;
        [SerializeField] private readonly int _maxReward = 100;
        [SerializeField] private readonly int _minReward = 10;
        [SerializeField] private readonly float _speed = 2;

        private Vector3 _centerPosition;
        private Vector3 _lastDirection;

        private Action<int> _onDeath;

        public void Initialize(Vector3 centerPosition, Action<int> onDeathCallback)
        {
            _centerPosition = centerPosition;
            _onDeath = onDeathCallback;

            transform.position = _centerPosition;
        }

        private void FixedUpdate()
        {
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            var direction = _centerPosition - transform.position;

            // otherwise direction is Vector3.zero when gameObject is located at spawn
            if (direction.magnitude == 0)
            {
                direction = Vector3.left;
                _lastDirection = Vector3.left;
            }

            // destroy ship if it's too far away
            if (direction.magnitude > _distanceDespawn)
            {
                Destroy(gameObject);
            }

            direction = Quaternion.Euler(0, _angleOfMovement, 0) * direction;

            transform.Translate(direction.normalized * _speed * Time.fixedDeltaTime, Space.World);
            transform.LookAt(_lastDirection.normalized + direction.normalized);
            _lastDirection = direction;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.GetComponent<Bullet>()) return;

            _onDeath?.Invoke(CalculateReward());
            Destroy(gameObject);
        }


        private int CalculateReward()
        {
            var distanceFromCenter = Mathf.Min(Vector3.Distance(_centerPosition, transform.position), _maxDistance);
            return (int)Mathf.Lerp(_maxReward, _minReward, distanceFromCenter / _maxDistance);
        }
    }
}