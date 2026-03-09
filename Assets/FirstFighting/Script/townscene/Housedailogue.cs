using UnityEngine;

public class Housedailogue : MonoBehaviour
{
    public GameObject dialogueUI; // Drag your Black Panel here

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Mike")
        {
            // 1. Show the Blackout UI
            dialogueUI.SetActive(true); 

            // 2. Stop Mike's feet from moving
            other.GetComponent<Mikemovement>().enabled = false;

            // 3. Freeze his physics so he stays in the red box
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            rb.linearVelocity = Vector2.zero; 
            rb.constraints = RigidbodyConstraints2D.FreezeAll; 
            
            Debug.Log("Mike is in the entrance. Starting conversation.");
        }
    }
}
