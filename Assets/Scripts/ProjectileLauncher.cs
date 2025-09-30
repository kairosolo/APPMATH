using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float launchSpeed = 5f;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private int rocketPerLaunch = 1;
    private List<float> angles = new List<float>();

    private void Start()
    {
        StartCoroutine(LaunchRocket());
    }

    private IEnumerator LaunchRocket()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireRate);

            angles.Clear();
            for (int i = 0; i < rocketPerLaunch; i++)
            {
                float angle = (360f / rocketPerLaunch) * i;
                angles.Add(angle);
            }

            for (int i = 0; i < angles.Count; i++)
            {
                float rad = angles[i] * Mathf.Deg2Rad;
                Vector2 direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;

                GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
                projectile.GetComponent<Projectile>().Initialize(direction * launchSpeed);
            }
        }
    }

    public void UpgradeRocketLauncher()
    {
        if (rocketPerLaunch < 8)
        {
            rocketPerLaunch++;
        }
    }

    public void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, player.rotation);
        projectile.GetComponent<Projectile>().Initialize(Vector2.up * launchSpeed);
    }
}