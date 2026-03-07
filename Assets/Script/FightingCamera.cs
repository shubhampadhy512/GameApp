using UnityEngine;

public class FightingCamera : MonoBehaviour
{
    public float baseWidth = 16f;
    public float baseHeight = 9f;

    void Start()
    {
        Camera cam = GetComponent<Camera>();

        float targetAspect = baseWidth / baseHeight;
        float screenAspect = (float)Screen.width / Screen.height;

        if (screenAspect >= targetAspect)
        {
            cam.orthographicSize = baseHeight / 2f;
        }
        else
        {
            float difference = targetAspect / screenAspect;
            cam.orthographicSize = (baseHeight / 2f) * difference;
        }
    }
}