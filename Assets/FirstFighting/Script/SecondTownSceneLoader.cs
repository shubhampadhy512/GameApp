using UnityEngine;
using UnityEngine.SceneManagement;

public class SecondTownSceneLoader : MonoBehaviour
{
    public void LoadTownScene()
    {
        SceneManager.LoadScene("Season2TownSceneLoader");
    }
}