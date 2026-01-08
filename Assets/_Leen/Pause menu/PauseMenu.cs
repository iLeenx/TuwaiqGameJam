using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuButton;
    public GameObject pauseMenuPanel;

    private bool isPaused = false;

    void Start()
    {
        ResumeGame();

        // set active state
        pauseMenuButton.SetActive(true); 
        pauseMenuPanel.SetActive(false);
    }

    void Update()
    {
        // escape key toggle
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    // called by pause button
    public void PauseGame()
    {
        isPaused = true;
        pauseMenuPanel.SetActive(true);
        pauseMenuButton.SetActive(false);
        Time.timeScale = 0f;
    }

    // called by resume button
    public void ResumeGame()
    {
        isPaused = false;
        pauseMenuPanel.SetActive(false);
        pauseMenuButton.SetActive(true);
        Time.timeScale = 1f;
    }
}
