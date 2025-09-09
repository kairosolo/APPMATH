using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.position;
    }
}