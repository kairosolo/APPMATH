using UnityEngine;

public class TowerProjectile : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float rotateSpeed = 5f;

    private void Start()
    {
        Invoke("DestroyThis", 3f);
    }

    private void Update()
    {
        if (enemy == null)
        {
            transform.position += transform.up * bulletSpeed * Time.deltaTime;
            return;
        }

        Vector2 direction = enemy.transform.position - transform.position;
        direction.Normalize();

        float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, -targetAngle);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

        transform.position += transform.up * bulletSpeed * Time.deltaTime;

        float enemyDistance = Mathf.Sqrt(
            Mathf.Pow(enemy.transform.position.x - transform.position.x, 2) +
            Mathf.Pow(enemy.transform.position.y - transform.position.y, 2)
        );

        if (enemyDistance < 0.3f)
        {
            enemy.TakeDamage();
            Destroy(gameObject);
        }
    }

    public void AssignEnemy(Enemy target)
    {
        enemy = target;
    }

    private void DestroyThis()
    {
        Destroy(gameObject);
    }
}