using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SwitchResult : MonoBehaviour
{
    public GameObject GoodEnd;
    public GameObject NormalEnd;
    public GameObject BadEnd;
    public TextMeshProUGUI finalPoint;

    private int score;

    void Start()
    {
        if (ScoreManager.Instance != null)
        {
            score = ScoreManager.Instance.GetScore();
        }
        else
        {
            Debug.LogWarning("⚠️ ScoreManager instance not found! Make sure it exists in a previous scene.");
            score = 0;
        }

        // Disable all images first
        GoodEnd.gameObject.SetActive(false);
        NormalEnd.gameObject.SetActive(false);
        BadEnd.gameObject.SetActive(false);

        // Show correct ending
        if (score >= 30)
            GoodEnd.gameObject.SetActive(true);
        else if (score >= 15)
            NormalEnd.gameObject.SetActive(true);
        else
            BadEnd.gameObject.SetActive(true);

        Debug.Log($"✅ Ending displayed for score {score}");

        finalPoint.text = score.ToString();
    }
}
