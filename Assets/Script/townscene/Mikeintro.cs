using UnityEngine;

public class Mikeintro : MonoBehaviour

   {
    public float roadY = -1.5f; // The street level
    public float fallSpeed = 10f;
    private bool hasLanded = false;

    void Update()
    {
        if (!hasLanded)
        {
            // Keep Z at 0 so he stays visible!
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;

            if (transform.position.y <= roadY)
            {
                // Land precisely on the road at Z: 0
                transform.position = new Vector3(transform.position.x, roadY, 0);
                hasLanded = true;
                
                // Enable your movement script now
                GetComponent<Mikemovement>().enabled = true; 
            }
        }
    }
}
