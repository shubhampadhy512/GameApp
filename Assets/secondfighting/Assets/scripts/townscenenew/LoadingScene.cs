using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Season2
{
    public class LoadingScene : MonoBehaviour
    {
        void Start()
        {
            StartCoroutine(LoadGame());
        }

        IEnumerator LoadGame()
        {
            yield return new WaitForSeconds(2f);

            AsyncOperation operation = SceneManager.LoadSceneAsync("SEASON2");

            while (!operation.isDone)
            {
                yield return null;
            }
        }
    }
}