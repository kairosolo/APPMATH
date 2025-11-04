using UnityEngine;

public class Enemy3D : MonoBehaviour
{
    [System.Serializable]
    public struct Fake3DPos
    {
        public float x, y, z;
    }

    public Fake3DPos fake3DPos;
    private CameraComponent cam;
    private Player3D player;

    [SerializeField] private float travelTime = 1.5f;
    private float lerpTimer;
    private Vector3 startPos;
    private Vector3 targetPos;
    private bool isMoving;

    [SerializeField] private float damageDistance = 0.5f;

    private bool hasSpawned = false;

    private void Awake()
    {
        cam = FindFirstObjectByType<CameraComponent>();
        player = FindFirstObjectByType<Player3D>();
    }

    private void Update()
    {
        if (!hasSpawned || player == null || player.pointList.Count == 0)
            return;

        HandleMovement();
        UpdateFake3DVisual();
        CheckDamage();

        if (!isMoving)
            ChooseNewTargetPoint();
    }

    private void HandleMovement()
    {
        if (!isMoving) return;

        lerpTimer += Time.deltaTime;
        float t = Mathf.Clamp01(lerpTimer / travelTime);
        Vector3 newPos = Vector3.Lerp(startPos, targetPos, t);

        fake3DPos.x = newPos.x;
        fake3DPos.y = newPos.y - 1;
        fake3DPos.z = newPos.z - 1.5f;

        if (t >= 1f)
        {
            isMoving = false;
            Destroy(gameObject);
        }
    }

    private void UpdateFake3DVisual()
    {
        float perspective = cam.GetPerspectve(fake3DPos.z);
        transform.position = new Vector2(fake3DPos.x, fake3DPos.y) * perspective;
        transform.localScale = Vector3.one * perspective;
    }

    private void CheckDamage()
    {
        if (player == null) return;

        float dx = fake3DPos.x - player.fake3DPos.x;
        float dy = fake3DPos.y - player.fake3DPos.y;
        float dz = fake3DPos.z - player.fake3DPos.z;
        float dist = Mathf.Sqrt(dx * dx + dy * dy + dz * dz);

        if (dist < damageDistance)
        {
            player.TakeDamage();
            Destroy(gameObject);
        }
    }

    private void ChooseNewTargetPoint()
    {
        if (player == null || player.pointList.Count == 0)
            return;

        startPos = new Vector3(fake3DPos.x, fake3DPos.y, fake3DPos.z);
        targetPos = player.pointList[Random.Range(0, player.pointList.Count)];

        lerpTimer = 0f;
        isMoving = true;
    }

    public void Set3DPosition(Vector3 spawnPos)
    {
        Debug.Log(spawnPos);
        fake3DPos.x = spawnPos.x;
        fake3DPos.y = spawnPos.y;
        fake3DPos.z = spawnPos.z;

        UpdateFake3DVisual();

        hasSpawned = true;
        ChooseNewTargetPoint();
    }
}