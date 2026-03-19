
using UnityEngine;

namespace Season2
{
    public class PlayerAudioManager2 : MonoBehaviour
    {
        public AudioSource audioSource;

        [Header("Player Sounds")]
        public AudioClip attackSound;
        public AudioClip hitSound;
        public AudioClip deathSound;

        public void PlayAttack()
        {
            if (attackSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(attackSound);
            }
        }

        public void PlayHit()
        {
            if (hitSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(hitSound);
            }
        }

        public void PlayDeath()
        {
            if (deathSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(deathSound);
            }
        }
    }
}

