
using System.Collections;
using UnityEngine;

public class ShowDeveloperName : MonoBehaviour
{
    public GameObject developerNameObject;
    public float showTime = 2f;

    public void ShowName()
    {
        StartCoroutine(ShowForSeconds());
    }

    IEnumerator ShowForSeconds()
    {
        developerNameObject.SetActive(true);

        yield return new WaitForSeconds(showTime);

        developerNameObject.SetActive(false);
    }
}
