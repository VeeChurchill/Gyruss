using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 2;

    private Vector3 _direction;

    public void Initialize(Vector3 target)
    {
        _direction = (target - transform.position);
        _direction.y = 0;
        _direction.Normalize();
    }

    private void FixedUpdate()
    {
        transform.position = transform.position + (_direction * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Destroy enemy on hit and score points
        if (other.gameObject.TryGetComponent(out EnemyController enemyController))
        {
            //enemyController.DestroyShip();
        }

        //Destroy bullet if center is reached
        if (other.gameObject.GetComponent<EnemySpawner>())
        {
            Destroy(gameObject);
        }
    }

}