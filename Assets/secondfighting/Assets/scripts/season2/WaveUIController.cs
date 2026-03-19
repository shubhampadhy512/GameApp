using System.Collections;
using UnityEngine;
using TMPro;

public class WaveUIController : MonoBehaviour
{
    public TextMeshProUGUI announcementText;
    public float showTime = 1.5f;
    public float fadeSpeed = 2f;

    public void ShowWave(int waveNumber)
    {
        StopAllCoroutines();
        StartCoroutine(ShowRoutine(waveNumber));
    }

    IEnumerator ShowRoutine(int waveNumber)
    {
        announcementText.text = "WAVE  "+ waveNumber ;

        Color c = announcementText.color;
        c.a = 1f;
        announcementText.color = c;

        yield return new WaitForSeconds(showTime);

        while (announcementText.color.a > 0)
        {
            c = announcementText.color;
            c.a -= Time.deltaTime * fadeSpeed;
            announcementText.color = c;

            yield return null;
        }
    }
}