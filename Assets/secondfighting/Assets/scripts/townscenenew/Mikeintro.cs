using UnityEngine;

namespace Season2
{
    public class Mikeintro : MonoBehaviour
    {
        public float roadY = -1.5f; // The street level
        public float fallSpeed = 10f;
        private bool hasLanded = false;

        void Update()
        {
            if (!hasLanded)
            {
                // Keep Z at 0 so he stays visible
                transform.position += Vector3.down * fallSpeed * Time.deltaTime;

                if (transform.position.y <= roadY)
                {
                    // Land precisely on the road
                    transform.position = new Vector3(transform.position.x, roadY, 0);
                    hasLanded = true;

                    // Enable movement script
                    GetComponent<Mikemovement>().enabled = true;
                }
            }
        }
    }
}