using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private int score = 0;
    public Text ScoreText;

    void Awake()
    {
        // ✅ Ensure a single persistent instance
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // <-- this keeps it alive across scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }

    void Update()
    {
        if (ScoreText != null)
            ScoreText.text = score.ToString();
    }

    public void AddScore()
    {
        score++;
        Debug.Log($"Current Score: {score}");
    }

    public int GetScore()
    {
        return score;
    }

    public void ResetScore()
    {
        score = 0;
    }
}
