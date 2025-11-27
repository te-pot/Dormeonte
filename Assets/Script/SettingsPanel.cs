using UnityEngine;

public class SettingsPanel : MonoBehaviour
{
    public GameObject Panel;      // The Settings UI panel
    public GameObject PauseMenu;  // The Pause Menu panel

    private bool isPaused = false;

    // Called from the "Settings" button in the pause menu
    public void OpenSettingsMenu()
    {
        ToggleSettingsMenu(true);
    }

    // Called from a "Back" or "Close" button in the settings menu (optional)
    public void CloseSettingsMenu()
    {
        ToggleSettingsMenu(false);
    }

    private void ToggleSettingsMenu(bool open)
    {
        isPaused = open;
        Panel.SetActive(open);
        PauseMenu.SetActive(!open);

        // Keep the game paused the whole time
        Time.timeScale = 0f;
    }
}
