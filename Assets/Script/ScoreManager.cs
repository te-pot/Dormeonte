using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private int score = 0;
    public Text ScoreText;

    void Awake()
    {
        // Ensure only one persistent instance
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Listen for scene reloads so we can reassign UI text automatically
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Try to find the ScoreText again in the new scene
        if (ScoreText == null)
        {
            Text[] texts = FindObjectsByType<Text>(FindObjectsSortMode.None);
            foreach (Text t in texts)
            {
                if (t.name == "ScoreText") // 👈 name of your UI Text object
                {
                    ScoreText = t;
                    break;
                }
            }
        }

        // Update the display right after loading
        UpdateScoreText();
    }

    private void Update()
    {
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (ScoreText != null)
            ScoreText.text = score.ToString();
    }

    public void AddScore()
    {
        score++;
        UpdateScoreText();
    }

    public void AddMultipleScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    public int GetScore()
    {
        return score;
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }
}
