using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private List<Transform> enemyList;
    public bool gameEnd = false;

    private void Update()
    {
        if (!gameEnd)
        {
            Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            transform.position += direction * 2 * Time.deltaTime;
        }

        foreach (var enemy in enemyList)
        {
            Vector3 originalPosition = enemy.localPosition;
            float distance = Mathf.Sqrt(Mathf.Pow(enemy.position.x - this.transform.position.x, 2) + Mathf.Pow(enemy.position.y - this.transform.position.y, 2));
            if (distance < 1.5f)
            {
                float xOffset = Random.Range(-.1f, .1f);
                float yOffset = Random.Range(-.1f, .1f);

                enemy.localPosition = originalPosition + new Vector3(xOffset, yOffset, 0f);
            }
            else
            {
                enemy.localPosition = enemy.GetComponent<Enemy>().originalPosition;
            }
            if (distance < .5f)
            {
                gameEnd = true;
                GameManager.Instance.Lose();
            }
        }

        float distanceToWin = Mathf.Sqrt(Mathf.Pow(target.position.x - this.transform.position.x, 2) + Mathf.Pow(target.position.y - this.transform.position.y, 2));
        if (distanceToWin < .5f)
        {
            gameEnd = true;
            GameManager.Instance.Win();
        }
    }
}