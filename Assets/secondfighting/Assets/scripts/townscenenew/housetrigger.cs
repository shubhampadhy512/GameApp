using UnityEngine;

namespace Season2
{
    public class housetrigger : MonoBehaviour
    {
        public dailoguemanager dialogueManagerScript;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.name == "Steve")
            {
                dialogueManagerScript.StartStory();

                // Trigger only once
                gameObject.SetActive(false);
            }
        }
    }
}