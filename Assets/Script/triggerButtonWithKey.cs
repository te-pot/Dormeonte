using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TriggerButtonWithKey : MonoBehaviour
{
    public KeyCode key = KeyCode.Escape;
    public GameObject PausePanel;
    public GameObject SettingsPanel;

    private bool isPaused = false;

    void Update()
    {
        // --- ESC key behavior ---
        if (Input.GetKeyDown(key))
        {
            // If settings menu is open, return to pause menu instead of unpausing
            if (SettingsPanel.activeSelf)
            {
                ReturnToPauseMenu();
            }
            else
            {
                TogglePauseMenu();
            }
        }

        // --- Allow opening pause with click (only if not paused) ---
        else if (Input.GetMouseButtonDown(0) && !isPaused)
        {
            // Only open pause if the click wasn't on a UI element
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                TogglePauseMenu();
            }
        }
    }

    void TogglePauseMenu()
    {
        isPaused = !isPaused;

        // Toggle pause panel visibility
        PausePanel.SetActive(isPaused);

        // Close settings menu if it's open
        if (!isPaused)
        {
            SettingsPanel.SetActive(false);
        }

        // Call button event when opening
        if (isPaused)
        {
            GetComponent<Button>()?.onClick.Invoke();
        }

        // Pause/unpause the game
        Time.timeScale = isPaused ? 0f : 1f;
    }

    // --- Called by the "Back" button in Settings menu ---
    public void ReturnToPauseMenu()
    {
        SettingsPanel.SetActive(false);
        PausePanel.SetActive(true);
    }
}
