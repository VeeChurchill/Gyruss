using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] private Bullet _bulletPrefab = null;
        [SerializeField] private Transform _bulletSpawnPoint = null;

        private Transform _target;

        public void Initialize(Transform target)
        {
            _target = target;
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            //Prevent double shoot on button up
            if (!context.performed) return;

            var bullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, Quaternion.identity);
            bullet.Initialize(_target.position);
        }
    }
}