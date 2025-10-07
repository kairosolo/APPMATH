using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private Transform currentTarget;

    [Header("Tower")]
    [SerializeField] private float rotSpeed = 5f;
    [SerializeField] private float shootRange = 5f;

    [Header("Projectile")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 0.5f;
    private float nextFireTime;

    private void Update()
    {
        List<Transform> enemies = EnemyManager.Instance.GetEnemyList();
        if (enemies == null || enemies.Count == 0)
        {
            currentTarget = null;
            return;
        }

        if (currentTarget == null || !enemies.Contains(currentTarget) ||
           Mathf.Sqrt(Mathf.Pow(currentTarget.transform.position.x - this.transform.position.x, 2) + Mathf.Pow(currentTarget.transform.position.y - this.transform.position.y, 2)) > shootRange)
        {
            currentTarget = GetNearestEnemy(enemies);
        }

        if (currentTarget == null) return;

        RotateTowardsTarget();

        Vector2 directionToTarget = (currentTarget.position - transform.position).normalized;
        if (Vector3.Dot(transform.up, directionToTarget) >= 0.98f)
        {
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    private Transform GetNearestEnemy(List<Transform> enemies)
    {
        Transform nearest = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Transform enemy in enemies)
        {
            if (enemy == null) continue;
            float distance = Mathf.Sqrt(Mathf.Pow(enemy.transform.position.x - this.transform.position.x, 2) + Mathf.Pow(enemy.transform.position.y - this.transform.position.y, 2));

            if (distance < shortestDistance && distance <= shootRange)
            {
                shortestDistance = distance;
                nearest = enemy;
            }
        }

        return nearest;
    }

    private void RotateTowardsTarget()
    {
        if (currentTarget == null) return;

        Vector2 direction = currentTarget.position - transform.position;
        float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        Quaternion targetRot = Quaternion.Euler(0, 0, -targetAngle);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, rotSpeed * Time.deltaTime);
    }

    private void Shoot()
    {
        Instantiate(projectilePrefab, firePoint.position, transform.rotation).TryGetComponent<TowerProjectile>(out TowerProjectile projectile);
        projectile.AssignEnemy(currentTarget.GetComponent<Enemy>());
    }
}