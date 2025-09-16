using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private float speed;

    private void Update()
    {
        float distance = Mathf.Sqrt(Mathf.Pow(player.transform.position.x - this.transform.position.x, 2) + Mathf.Pow(player.transform.position.y - this.transform.position.y, 2));

        if (distance > 1f)
        {
            Vector2 newPosition = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * speed);
            transform.position = newPosition;
        }

        if (distance < 1f)
        {
            player.gameEnd = true;
            GameManager.Instance.Lose();
        }
    }
}