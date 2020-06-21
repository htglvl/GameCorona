using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;
    public GameObject DeadMenuUI;
    public bool gameIsPause = false;
    public GameObject PauseMenuUI;
    public void EndGame()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            Debug.Log("Game Over");
            DeadMenuUI.SetActive(true);
            //DeadMenuUI.GetComponent<DeathScreen>().GetDeathStats();
            gameObject.GetComponent<BuyMenuAndHotkey>().enabled = false;
            gameObject.GetComponent<BuyMenuAndHotkey>().BuyMenu.SetActive(false);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && DeadMenuUI.activeInHierarchy)
        {
            Restart();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPause = true;
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPause = false;
    }
    public void Restart()
    {
        DeadMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPause = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Menu()
    {
        Time.timeScale = 1f;
        gameIsPause = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
