using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public float StartTimer = 3f;
    public Text StartTimerText;
    public GameObject ArrowPivot;
    public GameObject Spawners;
    public GameObject UIPanel;

    public float Timer = 60f;
    public Text TimerText;

    [Header("Fade Settings")]
    public Animator FadeAnimator;
    public string NextSceneName = "GameOver";

    private bool gameStarted = false;
    private bool isTransitioning = false;
    private ArrowShooter shooterScript;
    private ArrowSwing swingScript;

    private void Start()
    {
        shooterScript = ArrowPivot.GetComponent<ArrowShooter>();
        swingScript = ArrowPivot.GetComponent<ArrowSwing>();

        shooterScript.enabled = false;
        swingScript.enabled = false;
        UIPanel.gameObject.SetActive(false);
        Spawners.SetActive(true);
    }

    void Update()
    {
        if (isTransitioning) return; // 🔹 Stop everything during fade

        if (!gameStarted)
        {
            StartTimer -= Time.deltaTime;
            StartTimerText.text = Mathf.Ceil(StartTimer).ToString();

            if (StartTimer <= 0)
            {
                gameStarted = true;
                StartTimerText.text = "Start!";
                Invoke(nameof(HideStartText), 1f);

                shooterScript.enabled = true;
                swingScript.enabled = true;
                UIPanel.gameObject.SetActive(true);
            }
        }
        else
        {
            if (Timer > 0)
            {
                Timer -= Time.deltaTime;
                Timer = Mathf.Max(Timer, 0);
                TimerText.text = Mathf.Ceil(Timer).ToString();
            }

            // 🔹 Trigger fade only once
            if (Timer <= 0 && !isTransitioning)
            {
                StartCoroutine(HandleEndTransition());
            }
        }
    }

    private void HideStartText()
    {
        StartTimerText.gameObject.SetActive(false);
    }

    private System.Collections.IEnumerator HandleEndTransition()
    {
        isTransitioning = true;
        UIPanel.SetActive(false);
        Time.timeScale = 0f;
        FadeAnimator.SetTrigger("FadeOutTrigger");
        yield return new WaitForSecondsRealtime(3f);
        SceneManager.LoadScene(NextSceneName);
    }
}
