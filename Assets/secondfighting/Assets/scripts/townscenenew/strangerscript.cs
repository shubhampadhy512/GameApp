using UnityEngine;
using TMPro;
using System.Collections;

public class StrangerScript : MonoBehaviour
{
    public CanvasGroup masterUI;
    public TextMeshProUGUI textDisplay;
    public string[] storyLines;
    public float fadeSpeed = 1.5f;
    public float typingSpeed = 0.05f;

    public void StartStory()
    {
        gameObject.SetActive(true);
        StartCoroutine(ExecuteSequence());
    }

    IEnumerator ExecuteSequence()
    {
        while (masterUI.alpha < 1)
        {
            masterUI.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }

        foreach (string line in storyLines)
        {
            textDisplay.text = "";

            foreach (char letter in line)
            {
                textDisplay.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }

            yield return new WaitForSeconds(3f);
        }

        textDisplay.text = "";
        Debug.Log("Scene ready for fight.");
    }
}