using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private List<Enemy> enemyList = new List<Enemy>();

    private Vector2 moveDirection;

    public void Initialize(Vector2 direction)
    {
        moveDirection = direction.normalized;
    }

    private void Start()
    {
        enemyList.AddRange(FindObjectsByType<Enemy>(FindObjectsSortMode.None));
    }

    private void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);

        foreach (Enemy enemy in enemyList)
        {
            if (enemy == null) continue;

            float enemyDistance = Mathf.Sqrt(
                Mathf.Pow(enemy.transform.position.x - transform.position.x, 2) +
                Mathf.Pow(enemy.transform.position.y - transform.position.y, 2)
            );

            if (enemyDistance < 1f)
            {
                Destroy(enemy.gameObject);
                Destroy(gameObject);
                break;
            }
        }
    }
}