using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TownLoading : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadTown());
    }

    IEnumerator LoadTown()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("SEASON2TownScene");
    }
}