using UnityEngine;

public class HitChecker : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Something entered hitbox: " + collision.name);

        if (collision.CompareTag("Enemy"))
        {
            Demogorgan enemy = collision.GetComponentInParent<Demogorgan>();

            if (enemy != null)
            {
                Debug.Log("Calling Fall()");
                enemy.Fall();
            }
            else
            {
                Debug.Log("Demogorgan script not found on parent!");
            }
        }
    }

}
