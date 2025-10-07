using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    [SerializeField] private List<Transform> enemyList;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    public List<Transform> GetEnemyList()
    {
        return enemyList;
    }

    public void AddEnemy(Transform newEnemy)
    {
        enemyList.Add(newEnemy);
    }

    public void RemoveEnemy(Transform enemy)
    {
        enemyList.Remove(enemy);
    }
}