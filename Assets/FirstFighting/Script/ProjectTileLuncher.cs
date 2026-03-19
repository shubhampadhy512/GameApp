using UnityEngine;

public class ProjecTileLuncher : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform launchPoint;

    private PlayerAudioManager playerAudio;

    private void Awake()
    {
        playerAudio = GetComponent<PlayerAudioManager>();
    }

    // Called by Animation Event
    public void FireProjectile()
    {
        // PLAY SHOTGUN SOUND
        if (playerAudio != null)
        {
            playerAudio.PlayAttack();
        }

        // Spawn projectile
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, projectilePrefab.transform.rotation);

        Vector3 origScale = projectile.transform.localScale;

        // Flip projectile depending on player direction
        projectile.transform.localScale = new Vector3(
            origScale.x * (transform.localScale.x > 0 ? 1 : -1),
            origScale.y,
            origScale.z
        );
    }
}