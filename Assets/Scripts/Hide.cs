using UnityEngine;

public class ObjectHider : MonoBehaviour
{
    private Renderer objectRenderer;
    private bool isObjectHidden = false;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer == null)
        {
            Debug.LogError("Renderer component not found on the object.");
            enabled = false;
        }
    }

    void Update()
    {
        // press "H" in keyboard
        if (Input.GetKeyDown(KeyCode.H))
        {
            ToggleObjectVisibility();
        }
    }
    
    void ToggleObjectVisibility()
    {
        isObjectHidden = !isObjectHidden;
        if (objectRenderer != null)
        {
            objectRenderer.enabled = !isObjectHidden;
        }
        else
        {
            Debug.LogError("Renderer component not found on the object.");
        }
    }
}
