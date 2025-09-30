using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TowerProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Player player;
    private Vector2 moveDirection;

    public void Initialize(Vector2 direction)
    {
        moveDirection = direction.normalized;
    }

    private void Start()
    {
        player = FindFirstObjectByType<Player>();
    }

    private void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);

        if (player == null) return;

        float enemyDistance = Mathf.Sqrt(
            Mathf.Pow(player.transform.position.x - transform.position.x, 2) +
            Mathf.Pow(player.transform.position.y - transform.position.y, 2)
        );

        if (enemyDistance < 1f)
        {
            SceneManager.LoadScene(1);
            Destroy(player.gameObject);
            Destroy(gameObject);
        }
    }
}