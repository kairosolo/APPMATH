using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed;
    [SerializeField] private float rotSpeed;
    public bool gameEnd = false;

    private void Update()
    {
        if (!gameEnd)
        {
            Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            transform.position += moveDirection * speed * Time.deltaTime;
        }

        float distanceToWin = Mathf.Sqrt(Mathf.Pow(target.position.x - this.transform.position.x, 2) + Mathf.Pow(target.position.y - this.transform.position.y, 2));
        if (distanceToWin < .5f)
        {
            gameEnd = true;
            GameManager.Instance.Win();
        }
    }
}