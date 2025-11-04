using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player3D : MonoBehaviour
{
    [System.Serializable]
    public struct Fake3DPos
    { public float x, y, z; }

    public Fake3DPos fake3DPos; private CameraComponent cam;

    [Header("Movement")]
    [SerializeField] private float travelTime = 1f;
    private float lerpTimer; private Vector3 startPos;
    private bool isMoving;
    public List<Vector3> pointList = new List<Vector3>();
    private int currentPoint = 0;

    [Header("UI")]
    [SerializeField] private int health;
    [SerializeField] private TextMeshProUGUI healthText;

    private void Start()
    {
        cam = FindAnyObjectByType<CameraComponent>();
        if (pointList.Count > 0)
        {
            fake3DPos.x = pointList[0].x;
            fake3DPos.y = pointList[0].y;
            fake3DPos.z = pointList[0].z;
            UpdateFake3DVisual();
        }
    }

    private void Update()
    {
        HandleInput();
        HandleMovement();
        UpdateFake3DVisual();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (currentPoint < pointList.Count - 1)
            {
                currentPoint++;
                PointSwitched();
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (currentPoint > 0)
            {
                currentPoint--;
                PointSwitched();
            }
        }
    }

    private void HandleMovement()
    {
        if (!isMoving) return;
        lerpTimer += Time.deltaTime; float t = Mathf.Clamp01(lerpTimer / travelTime);
        Vector3 target = pointList[currentPoint];
        Vector3 newPos = Vector3.Lerp(startPos, target, t);
        fake3DPos.x = newPos.x;
        fake3DPos.y = newPos.y;
        fake3DPos.z = newPos.z;
        if (t >= 1f) isMoving = false;
    }

    private void UpdateFake3DVisual()
    {
        float perspective = cam.GetPerspectve(fake3DPos.z);
        transform.position = new Vector2(fake3DPos.x, fake3DPos.y) * perspective;
        transform.localScale = Vector3.one * perspective;
    }

    private void PointSwitched()
    {
        startPos = new Vector3(fake3DPos.x, fake3DPos.y, fake3DPos.z);
        lerpTimer = 0f; isMoving = true;
    }

    public void TakeDamage()
    {
        health -= 10;
        healthText.text = $"Health: {health}";
    }
}