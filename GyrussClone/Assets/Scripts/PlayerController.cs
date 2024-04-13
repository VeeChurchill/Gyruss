using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerCombat _playerCombat = null;
        [SerializeField] private PlayerMovement _playerMovement = null;
        [SerializeField] private Transform _targetTransform = null;


        private void Awake()
        {
            _playerCombat.Initialize(_targetTransform);
            _playerMovement.Initialize(_targetTransform);
        }
    }
}