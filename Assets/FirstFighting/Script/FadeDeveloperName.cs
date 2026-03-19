
using System.Collections;
using UnityEngine;

public class FadeDeveloperName : MonoBehaviour
{
    public GameObject developerNameObject;
    public CanvasGroup developerNameCanvas;

    public float fadeDuration = 0.5f;
    public float showTime = 2f;

    public void ShowDeveloper()
    {
        StartCoroutine(FadeRoutine());
    }

    IEnumerator FadeRoutine()
    {
        // Enable object first
        developerNameObject.SetActive(true);

        float t = 0;

        // Fade In
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            developerNameCanvas.alpha = Mathf.Lerp(0, 1, t / fadeDuration);
            yield return null;
        }

        developerNameCanvas.alpha = 1;

        // Stay visible
        yield return new WaitForSeconds(showTime);

        t = 0;

        // Fade Out
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            developerNameCanvas.alpha = Mathf.Lerp(1, 0, t / fadeDuration);
            yield return null;
        }

        developerNameCanvas.alpha = 0;

        // Disable again
        developerNameObject.SetActive(false);
    }
}
