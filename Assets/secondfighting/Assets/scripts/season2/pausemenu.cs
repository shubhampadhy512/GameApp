using UnityEngine;
using UnityEngine.SceneManagement;

namespace Season2
{
    public class pausemenu : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenuUI;

        public void Pause()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
        }

        public void Resume()
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
        }

        public void Restart()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}