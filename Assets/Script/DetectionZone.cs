using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public List<Collider2D> detectedColliders = new List<Collider2D>();

    private Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void Update()
    {
        // 🔥 Remove dead or null objects automatically
        detectedColliders.RemoveAll(c =>
        {
            if (c == null) return true;

            Damageable d = c.GetComponentInParent<Damageable>();
            return d == null || !d.IsAlive;
        });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponentInParent<Damageable>();

        // Only add if object has Damageable AND is alive
        if (damageable != null && damageable.IsAlive)
        {
            if (!detectedColliders.Contains(collision))
            {
                detectedColliders.Add(collision);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (detectedColliders.Contains(collision))
        {
            detectedColliders.Remove(collision);
        }
    }
}