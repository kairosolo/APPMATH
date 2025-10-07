using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int health = 5;
    private int currentHealth;
    [SerializeField] private Image frontHealthBar;
    [SerializeField] private Image ghostHealthBar;
    [SerializeField] private float ghostLerpSpeed = 2f;

    [Header("Path Settings")]
    [SerializeField] private float travelTime = 1f;
    [SerializeField] private int currentIndex;
    [SerializeField] private Transform currentPath;

    private float lerpTimer;
    private Vector2 startPos;
    private bool isMoving;

    private void Start()
    {
        currentHealth = health;
        currentPath = PathHandler.Instance.GetPath(currentIndex);
        startPos = transform.position;
        isMoving = true;

        frontHealthBar.fillAmount = 1f;
        ghostHealthBar.fillAmount = 1f;
    }

    public void TakeDamage()
    {
        currentHealth = Mathf.Max(0, currentHealth - 1);

        float targetFill = (float)currentHealth / health;
        frontHealthBar.fillAmount = targetFill;

        if (currentHealth <= 0)
        {
            EnemyManager.Instance.RemoveEnemy(transform);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (ghostHealthBar.fillAmount > frontHealthBar.fillAmount)
        {
            ghostHealthBar.fillAmount = Mathf.Lerp(
                ghostHealthBar.fillAmount,
                frontHealthBar.fillAmount,
                Time.deltaTime * ghostLerpSpeed
            );
        }

        if (!isMoving || currentPath == null) return;

        lerpTimer += Time.deltaTime;
        float t = lerpTimer / travelTime;
        transform.position = Vector2.Lerp(startPos, currentPath.position, t);

        if (t >= 1f)
        {
            transform.position = currentPath.position;
            NextPath();
        }
    }

    private void NextPath()
    {
        currentIndex++;
        if (currentIndex >= PathHandler.Instance.pathLength)
        {
            Destroy(gameObject);
            return;
        }

        currentPath = PathHandler.Instance.GetPath(currentIndex);
        startPos = transform.position;
        lerpTimer = 0f;
        isMoving = true;
    }
}