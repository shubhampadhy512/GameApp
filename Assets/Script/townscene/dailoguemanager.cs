using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI; // Needed for the Button

public class dailoguemanager : MonoBehaviour
{
 public GameObject dailogueManagerUI; 
 public TextMeshProUGUI textDisplay;
public string[] storyLines;
 public float typingSpeed = 0.05f;

 [Header("New Choice Panel Settings")]
public GameObject choicePanel; // Drag your 'choicepanel' here
public TextMeshProUGUI choiceTextDisplay; // Drag the Text inside choicepanel here
 public Button challengeButton; // Drag the 'YESbutton' here

private CanvasGroup canvasGroup;

 void Start()
 {
 canvasGroup = GetComponent<CanvasGroup>();
if(choicePanel != null) choicePanel.SetActive(false); // Keep hidden at start
 }

 public void StartStory()
    {
        textDisplay.text = ""; 
        dailogueManagerUI.SetActive(true);
        StartCoroutine(FadeAndStackType());
    }

IEnumerator FadeAndStackType()
 {
        // 1. MAKE THE RED SHADE APPEAR
 float timer = 0;
 while (timer < 1f)
 {
timer += Time.deltaTime;
 canvasGroup.alpha = timer;
 yield return null;
   }

        // 2. TYPE LINES ONE AFTER ANOTHER (Mike & Dustin)
 foreach (string line in storyLines)
 {
 if (textDisplay.text != "") {
textDisplay.text += "\n\n"; 
 }

 foreach (char letter in line.ToCharArray())
{
 textDisplay.text += letter;
 yield return new WaitForSeconds(typingSpeed);
 }
 yield return new WaitForSeconds(2f); 
 }

        // --- NEW PART STARTS HERE ---

        // 3. VANISH THE PREVIOUS DIALOGUE
 dailogueManagerUI.SetActive(false); // The Mike/Dustin box disappears

        // 4. POP UP THE NEW PANEL
 choicePanel.SetActive(true); // Your duplicated red box appears
 choiceTextDisplay.text = ""; // Clear the "Are you ready" box

        // 5. TYPE THE CHALLENGE
 string challenge = "Are you ready to fight?";
 foreach (char letter in challenge.ToCharArray())
 {
 choiceTextDisplay.text += letter;
 yield return new WaitForSeconds(typingSpeed);
 }

        // 6. ADD THE "CHALLENGE ACCEPTED >>"
        // This adds it on the SAME line after a few spaces
 choiceTextDisplay.text += "  <color=red>CHALLENGE ACCEPTED >></color>";

        // Enable the button logic so the user can click the text
challengeButton.gameObject.SetActive(true);
}
}