using UnityEngine;

public class WorldScaler : MonoBehaviour
{
    public float referenceWidth = 1920f;
    public float referenceHeight = 1080f;

    void Start()
    {
        float screenRatio = (float)Screen.width / Screen.height;
        float targetRatio = referenceWidth / referenceHeight;

        if (screenRatio >= targetRatio)
        {
            float scaleHeight = screenRatio / targetRatio;
            transform.localScale = new Vector3(scaleHeight, scaleHeight, 1);
        }
        else
        {
            float scaleWidth = targetRatio / screenRatio;
            transform.localScale = new Vector3(scaleWidth, scaleWidth, 1);
        }
    }
}