using UnityEngine;

public class housetrigger : MonoBehaviour
{
    public dailoguemanager dialogueManagerScript;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Change "Mike" to "Steve" here!
        if (other.gameObject.name == "Steve") 
        {
            dialogueManagerScript.StartStory();
            
            // This turns off the trigger so it only happens once
            gameObject.SetActive(false); 
        }
    }
}

