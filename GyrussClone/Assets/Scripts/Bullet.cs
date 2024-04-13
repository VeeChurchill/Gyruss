using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Collider))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed = 2;

        private Vector3 _direction;

        public void Initialize(Vector3 target)
        {
            _direction = target - transform.position;
            _direction.y = 0;
            _direction.Normalize();
        }

        private void FixedUpdate()
        {
            transform.position += _direction * _speed * Time.fixedDeltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            //Destroy bullet if center is reached or enemy is hit
            if (other.gameObject.GetComponent<EnemySpawner>() || other.gameObject.GetComponent<EnemyController>())
            {
                Destroy(gameObject);
            }
        }
    }
}