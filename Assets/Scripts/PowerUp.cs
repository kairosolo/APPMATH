using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private ProjectileLauncher rocketLauncher;
    private bool interacted = false;

    private void Update()
    {
        float distance = Mathf.Sqrt(Mathf.Pow(player.transform.position.x - this.transform.position.x, 2) + Mathf.Pow(player.transform.position.y - this.transform.position.y, 2));
        if (distance < .5f)
        {
            if (interacted) return;

            interacted = true;
            rocketLauncher.UpgradeRocketLauncher();
            Destroy(gameObject);
        }
    }
}