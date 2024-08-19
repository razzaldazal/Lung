using UnityEngine;
using TMPro;

public class TextVisibilityController : MonoBehaviour
{
    public TextMeshPro attachedText;
    public float raycastOffset = 0.1f;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        if (attachedText == null)
        {
            Debug.LogError("Please assign a TextMeshPro component to the attachedText field.");
        }
    }

    void Update()
    {
        if (attachedText != null)
        {
            Vector3 directionToText = attachedText.transform.position - mainCamera.transform.position;
            Vector3 rayStart = mainCamera.transform.position + directionToText.normalized * raycastOffset;

            RaycastHit hit;
            if (Physics.Raycast(rayStart, directionToText, out hit, directionToText.magnitude))
            {
                attachedText.enabled = (hit.transform == transform);
            }
            else
            {
                attachedText.enabled = true;
            }
        }
    }
}
