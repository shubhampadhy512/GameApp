using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class IntroLoadingScene : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadGame());
    }

    IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(2f);

        AsyncOperation operation = SceneManager.LoadSceneAsync("TOWNSCENE1");

        while (!operation.isDone)
        {
            yield return null;
        }
    }
}