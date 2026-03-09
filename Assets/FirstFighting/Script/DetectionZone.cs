using System.Collections.Generic;
using UnityEngine;

// DetectionZone.cs
public class DetectionZone : MonoBehaviour
{
    public List<Collider2D> detectedColliders = new List<Collider2D>();

    private void Update()
    {
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
        if (damageable != null && damageable.IsAlive)
        {
            if (!detectedColliders.Contains(collision))
                detectedColliders.Add(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        detectedColliders.Remove(collision);
    }
}
