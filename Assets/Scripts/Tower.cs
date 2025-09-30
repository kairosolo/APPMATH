using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Tower")]
    [SerializeField] private Transform target;
    [SerializeField] private float rotSpeed;
    [SerializeField] private float shootRange;

    [Header("Projectile")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float bulletSpeed;
    private float nextFireTime;

    private void Update()
    {
        if (target == null) return;

        float distance = Mathf.Sqrt(Mathf.Pow(target.transform.position.x - this.transform.position.x, 2) + Mathf.Pow(target.transform.position.y - this.transform.position.y, 2));

        if (distance < shootRange)
        {
            var direction = target.position - transform.position;
            direction.Normalize();
            var targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            var rot = Quaternion.Euler(0, 0, -targetAngle);
            this.transform.rotation = Quaternion.Lerp(transform.rotation, rot, rotSpeed * Time.deltaTime);
            Vector2 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Dot(this.transform.up, directionToTarget) >= .98f)
            {
                if (Time.time >= nextFireTime)
                {
                    Shoot();
                    nextFireTime = Time.time + fireRate;
                }
            }
        }
    }

    public void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, transform.rotation);
        projectile.GetComponent<TowerProjectile>().Initialize(Vector2.up * bulletSpeed);
    }
}