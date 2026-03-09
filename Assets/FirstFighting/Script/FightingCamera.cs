using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FightingCamera : MonoBehaviour
{
    public float baseWidth = 16f;
    public float baseHeight = 10f;

    private Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
        AdjustCamera();
    }

    void AdjustCamera()
    {
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