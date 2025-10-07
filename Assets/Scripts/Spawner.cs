using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform enemyPrefab;
    [SerializeField] private int cooldown;
    private float currentCooldown;
    [SerializeField] private Transform spawnPoint;

    private void Update()
    {
        if (Time.time >= currentCooldown)
        {
            SpawnEnemy();
            currentCooldown = Time.time + cooldown;
        }
    }

    public void SpawnEnemy()
    {
        Transform enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity).transform;
        EnemyManager.Instance.AddEnemy(enemy);
    }
}