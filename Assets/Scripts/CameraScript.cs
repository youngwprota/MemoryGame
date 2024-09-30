using UnityEngine;

public class CameraResolutionHandler : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
        AdjustCameraSize();
    }

    void AdjustCameraSize()
    {
        float targetAspect = 1600.0f / 1200.0f;  // Целевое соотношение сторон
        float windowAspect = (float)Screen.width / (float)Screen.height;  // Текущее соотношение сторон экрана
        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1.0f)
        {
            // Если экран уже по вертикали, увеличиваем размер камеры
            mainCamera.orthographicSize = mainCamera.orthographicSize / scaleHeight;
        }
    }
}
