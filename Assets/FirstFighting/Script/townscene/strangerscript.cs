using UnityEngine;
using TMPro;
using System.Collections;
public class strangerscript : MonoBehaviour
{
    public CanvasGroup masterUI;      // The "Dimmer Switch"
    public TextMeshProUGUI textDisplay; 
    public string[] storyLines;
    public float fadeSpeed = 1.5f;    // How fast the red shade appears
    public float typingSpeed = 0.05f;

    public void StartStory()
    {
        gameObject.SetActive(true); 
        StartCoroutine(ExecuteSequence());
    }

    IEnumerator ExecuteSequence()
    {
        // 1. DIM THE SCREEN TO RED
        while (masterUI.alpha < 1)
        {
            masterUI.alpha += Time.deltaTime * fadeSpeed;
            yield return null; 
        }

        // 2. SHOW THE DIALOGUE
        foreach (string line in storyLines)
        {
            textDisplay.text = ""; 
            foreach (char letter in line.ToCharArray())
            {
                textDisplay.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
            yield return new WaitForSeconds(3f); // Wait for player to read
        }

        // 3. STAY IN THE RED SHADE
        textDisplay.text = ""; 
        Debug.Log("Scene is now ready for the fight transition.");
    }
}