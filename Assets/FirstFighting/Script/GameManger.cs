using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;

    void Start()
    {
        // Hide and lock cursor at start
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
    }

    void Update()
    {
        if (gameOverUI != null && gameOverUI.activeInHierarchy)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void gameOver()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
    }

    public void restart()
    {
        Debug.Log("Restarting...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void mainMenu()
    {
        Debug.Log("Loading Main Menu...");
        SceneManager.LoadScene("main menu");
    }

    public void quit()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}