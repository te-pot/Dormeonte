using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SwitchResult : MonoBehaviour
{
    public GameObject GoodEnd;
    public GameObject NormalEnd;
    public GameObject BadEnd;
    public TextMeshProUGUI finalPoint;

    private int score = 0;

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

        GoodEnd.gameObject.SetActive(false);
        NormalEnd.gameObject.SetActive(false);
        BadEnd.gameObject.SetActive(false);

        if (score >= 250)
            GoodEnd.gameObject.SetActive(true);
        else if (score >= 100)
            NormalEnd.gameObject.SetActive(true);
        else
            BadEnd.gameObject.SetActive(true);

        Debug.Log($"✅ Ending displayed for score {score}");

        finalPoint.text = score.ToString();
    }
}
