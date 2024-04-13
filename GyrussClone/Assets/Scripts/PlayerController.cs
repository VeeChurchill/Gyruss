using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerCombat _playerCombat;

    
    void Awake()
    {
        _playerCombat.Initialize(_targetTransform);
        _playerMovement.Initialize(_targetTransform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
