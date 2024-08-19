using UnityEngine;

public class CursorZoom : MonoBehaviour
{
    public Camera mainCamera;
    public float zoomSpeed = 1f;
    public float minZoom = 2f;
    public float maxZoom = 20f;

    private float currentZoom;

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        currentZoom = mainCamera.orthographicSize;
    }

    void Update()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0)
        {
            ZoomCamera(scrollInput);
        }
    }

    void ZoomCamera(float increment)
    {
        Vector3 mouseWorldPosBefore = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        currentZoom = Mathf.Clamp(currentZoom - increment * zoomSpeed, minZoom, maxZoom);
        mainCamera.orthographicSize = currentZoom;

        Vector3 mouseWorldPosAfter = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        mainCamera.transform.position += mouseWorldPosBefore - mouseWorldPosAfter;
    }
}
