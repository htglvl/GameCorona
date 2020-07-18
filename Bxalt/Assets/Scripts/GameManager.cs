using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;
    public GameObject DeadMenuUI;
    public bool gameIsPause = false;
    public GameObject PauseMenuUI;
    public TrackAIStats trackAIStats;

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
        if (trackAIStats.SoAI == trackAIStats.SonguoiDcBaoVe && trackAIStats.SonguoiCanTiemPhong == 0 && trackAIStats.SoNguoiBiBenh == 0 && trackAIStats.SoAI != 0)
        {
            EndGame();
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
        PhotonNetwork.LeaveRoom();
        Application.Quit();
    }
    public void Menu()
    {
        PhotonNetwork.LeaveRoom();
        Time.timeScale = 1f;
        gameIsPause = false;
        SceneManager.LoadScene(0);
    }
}
