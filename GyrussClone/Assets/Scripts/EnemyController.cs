using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float _angleOfMovement = 100f;
        [SerializeField] private float _maxDistance = 10f;
        [SerializeField] private int _maxReward = 100;
        [SerializeField] private int _minReward = 10;
        [SerializeField] private float _speed = 2;

        private Vector3 _centerPosition;
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
            if (direction.magnitude == 0) direction = Vector3.left;

            direction = Quaternion.Euler(0, _angleOfMovement, 0) * direction;

            transform.Translate(direction.normalized * _speed * Time.fixedDeltaTime, Space.World);
            transform.LookAt(direction);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Bullet>()) DestroyShip();
        }

        public void DestroyShip()
        {
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