using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip attackSound;
    public AudioClip hitSound;
    public AudioClip deathSound;

    public void PlayAttack()
    {
        if (attackSound != null)
            audioSource.PlayOneShot(attackSound);
    }

    public void PlayHit()
    {
        if (hitSound != null)
            audioSource.PlayOneShot(hitSound);
    }

    public void PlayDeath()
    {
        if (deathSound != null)
            audioSource.PlayOneShot(deathSound);
    }
}