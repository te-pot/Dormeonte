using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float StartTimer = 3f;
    public Text StartTimerText;
    public GameObject ArrowPivot;
    public GameObject Spawners;

    public float Timer = 60f;
    public Text TimerText;

    private bool gameStarted = false;
    private ArrowShooter shooterScript;
    private ArrowSwing swingScript;

    private void Start()
    {
        // ✅ Get the actual components from the scene object
        shooterScript = ArrowPivot.GetComponent<ArrowShooter>();
        swingScript = ArrowPivot.GetComponent<ArrowSwing>();

        // ✅ Disable the gameplay scripts and timer UI until countdown finishes
        shooterScript.enabled = false;
        swingScript.enabled = false;
        TimerText.gameObject.SetActive(false);
        Spawners.SetActive(true);
    }

    void Update()
    {
        if (!gameStarted)
        {
            StartTimer -= Time.deltaTime;
            StartTimerText.text = Mathf.Ceil(StartTimer).ToString();

            if (StartTimer <= 0)
            {
                gameStarted = true;
                StartTimerText.text = "Start!";
                Invoke(nameof(HideStartText), 1f);

                // ✅ Enable the gameplay after the countdown
                shooterScript.enabled = true;
                swingScript.enabled = true;
                TimerText.gameObject.SetActive(true);
            }
        }
        else
        {
            // ✅ Main timer runs during gameplay
            Timer -= Time.deltaTime;
            Timer = Mathf.Max(Timer, 0);
            TimerText.text = Mathf.Ceil(Timer).ToString();

            if (Timer <= 0)
            {
                Debug.Log("Timer ended!");
                enabled = false;
            }
        }
    }

    private void HideStartText()
    {
        StartTimerText.gameObject.SetActive(false);
    }
}
