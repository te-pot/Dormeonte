using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private  int score = 0;
    public Text ScoreText;

    void Start()
    {
    }
    
    void Update()
    {
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

}
