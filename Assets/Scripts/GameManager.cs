using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameObject canvas;
    [SerializeField] private TextMeshProUGUI resultText;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void Win()
    {
        canvas.SetActive(true);
        resultText.text = "YOU WIN!";
    }

    public void Lose()
    {
        canvas.SetActive(true);
        resultText.text = "YOU LOSE!";
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}