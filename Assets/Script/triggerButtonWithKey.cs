using UnityEngine;
using UnityEngine.UI;

public class TriggerButtonWithKey : MonoBehaviour
{
    public KeyCode key = KeyCode.Escape;
    public GameObject PausePanel;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            TogglePauseMenu();
        }
    }

    void TogglePauseMenu()
    {
        isPaused = !isPaused;
        PausePanel.SetActive(isPaused);

        // ✅ Only call the button if you're *opening* the pause menu
        if (isPaused)
        {
            GetComponent<Button>()?.onClick.Invoke();
        }

        // ✅ Optional: pause and unpause the game
        Time.timeScale = isPaused ? 0f : 1f;
    }
}
