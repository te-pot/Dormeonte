using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;

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
