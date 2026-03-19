using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static string sceneToLoad;

    public void LoadFightScene()
    {
        sceneToLoad = "SEASON1";
         SceneManager.LoadScene("LoadingScene2");
    }
}