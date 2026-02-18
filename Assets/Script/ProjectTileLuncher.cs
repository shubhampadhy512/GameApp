using UnityEngine;

public class ProjectTileLuncher : MonoBehaviour
{
    public GameObject projectilePrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void FiredProjectile()
    {
        Instantiate(projectilePrefab,transform.position,projectilePrefab.transform.rotation);
    }
}
