using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform target;
    public bool gameEnd = false;

    private void Update()
    {
        if (!gameEnd)
        {
            Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            transform.position += direction * 2 * Time.deltaTime;
        }

        float distanceToWin = Mathf.Sqrt(Mathf.Pow(target.position.x - this.transform.position.x, 2) + Mathf.Pow(target.position.y - this.transform.position.y, 2));
        if (distanceToWin < .5f)
        {
            gameEnd = true;
            GameManager.Instance.Win();
        }
    }
}