using UnityEngine;
using UnityEngine.Rendering.Universal; 
public class flicker2d : MonoBehaviour
{
    private Light2D myLight;
    public float stopAfterSeconds = 4f; // Stops after 4 seconds
    public float blinkSpeed = 2f;       // How fast it "breathes"
    private float timer = 0f;

    void Start()
    {
        myLight = GetComponent<Light2D>(); // Connects to the light
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer < stopAfterSeconds)
        {
            // This creates a smooth "breathing" effect using a Sine wave
            float lerp = Mathf.PingPong(Time.time * blinkSpeed, 1);
            myLight.intensity = Mathf.Lerp(1f, 6f, lerp); 
        }
        else
        {
            // After 4 seconds, stay at a steady intensity so Mike can find it
            myLight.intensity = 5f; 
            this.enabled = false; // Turns off the script to save power
        }
    }
}
