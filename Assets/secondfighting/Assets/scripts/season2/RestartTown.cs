using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartTown : MonoBehaviour
{
    public void LoadTownScene()
    {
        SceneManager.LoadScene("LoadingScene");
    }
}