using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

namespace Season2
{
  public class dailoguemanager : MonoBehaviour
  {
    public GameObject dailogueManagerUI;
    public TextMeshProUGUI textDisplay;
    public string[] storyLines;
    public float typingSpeed = 0.05f;

    [Header("New Choice Panel Settings")]
    public GameObject choicePanel;
    public TextMeshProUGUI choiceTextDisplay;
    public Button challengeButton;

    private CanvasGroup canvasGroup;

    void Start()
    {
      canvasGroup = GetComponent<CanvasGroup>();

      if (choicePanel != null)
        choicePanel.SetActive(false);
    }

    public void StartStory()
    {
      textDisplay.text = "";
      dailogueManagerUI.SetActive(true);
      StartCoroutine(FadeAndStackType());
    }

    IEnumerator FadeAndStackType()
    {
      float timer = 0;

      while (timer < 1f)
      {
        timer += Time.deltaTime;
        canvasGroup.alpha = timer;
        yield return null;
      }

      foreach (string line in storyLines)
      {
        if (textDisplay.text != "")
        {
          textDisplay.text += "\n\n";
        }

        foreach (char letter in line.ToCharArray())
        {
          textDisplay.text += letter;
          yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(2f);
      }

      dailogueManagerUI.SetActive(false);

      choicePanel.SetActive(true);
      choiceTextDisplay.text = "";

      string challenge = "Are you ready to fight?";

      foreach (char letter in challenge.ToCharArray())
      {
        choiceTextDisplay.text += letter;
        yield return new WaitForSeconds(typingSpeed);
      }

      choiceTextDisplay.text += "  <color=red>CHALLENGE ACCEPTED >></color>";

      challengeButton.gameObject.SetActive(true);
    }
  }
}