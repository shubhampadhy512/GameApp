using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    public AudioSource audioSource;

    [Header("Player Sounds")]
    public AudioClip attackSound;
    public AudioClip hitSound;
    public AudioClip jumpSound;
    public AudioClip deathSound;

    public void PlayAttack()
    {
        audioSource.PlayOneShot(attackSound);
    }

    public void PlayHit()
    {
        audioSource.PlayOneShot(hitSound);
    }

    public void PlayJump()
    {
        audioSource.PlayOneShot(jumpSound);
    }

    public void PlayDeath()
    {
        audioSource.PlayOneShot(deathSound);
    }
}