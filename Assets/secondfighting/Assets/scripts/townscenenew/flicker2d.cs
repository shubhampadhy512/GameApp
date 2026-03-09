using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Season2
{
    public class flicker2d : MonoBehaviour
    {
        private Light2D myLight;
        public float stopAfterSeconds = 4f; // Stops after 4 seconds
        public float blinkSpeed = 2f;       // How fast it "breathes"
        private float timer = 0f;

        void Start()
        {
            myLight = GetComponent<Light2D>();
        }

        void Update()
        {
            timer += Time.deltaTime;

            if (timer < stopAfterSeconds)
            {
                float lerp = Mathf.PingPong(Time.time * blinkSpeed, 1);
                myLight.intensity = Mathf.Lerp(1f, 6f, lerp);
            }
            else
            {
                myLight.intensity = 5f;
                this.enabled = false;
            }
        }
    }
}