using UnityEngine;
using UnityEngine.SceneManagement;

namespace Season2
{
    public class SceneLoader : MonoBehaviour
    {
        public static string sceneToLoad;

        public void LoadFightScene()
        {
            sceneToLoad = "SEASON2";
            SceneManager.LoadScene("LoadingScene2");
        }
    }
}