using System.Collections.Generic;
using UnityEngine;

public class Spawner3D : MonoBehaviour
{
    [SerializeField] private Enemy3D enemy3DPrefab;
    [SerializeField] private float cooldown;
    private float currentCooldown;
    public List<Vector3> spawnPointList;

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
        Enemy3D enemy3D = Instantiate(enemy3DPrefab, transform.position, Quaternion.identity);
        enemy3D.Set3DPosition(spawnPointList[Random.Range(0, spawnPointList.Count)]);
    }
}