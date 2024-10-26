using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastInteraction : MonoBehaviour
{
    [SerializeField] private LayerMask interactableLayer; // Layer for interactable objects
    [SerializeField] private Color highlightColor = Color.white; // Color used to highlight interactable objects

    private Camera mainCamera;
    private GameObject lastInteractedObject; // Stores the last interacted object
    private Color originalColor; // Stores the original color of the object
    private InputAction mousePositionAction; // Action for getting mouse position

    private void Awake()
    {
        // Initialize the main camera and get the mouse position action from PlayerInput
        mainCamera = Camera.main;
        var playerInput = GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            mousePositionAction = playerInput.actions["MousePosition"];
        }
    }

    private void Update()
    {
        // Skip if no mouse position action is available
        if (mousePositionAction == null)
            return;

        // Get the current mouse position and create a ray from the camera
        Vector2 mousePosition = mousePositionAction.ReadValue<Vector2>();
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);

        // Reset the color of the previous object if there was one
        if (lastInteractedObject != null)
        {
            ResetColor();
        }

        // Perform a raycast that only detects objects on the interactable layer
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, interactableLayer))
        {
            GameObject hitObject = hit.collider.gameObject;

            // Find the root object if it consists of multiple elements
            Transform rootObject = hitObject.transform.root;

            // Store reference to the last interacted (root) object
            lastInteractedObject = rootObject.gameObject;

            // Highlight all child objects
            Renderer[] renderers = lastInteractedObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                // Check if the material supports the _Color property
                if (renderer.material.HasProperty("_Color") && ((1 << renderer.gameObject.layer) & interactableLayer) != 0)
                {
                    originalColor = renderer.material.color;
                    renderer.material.color = highlightColor;
                }
            }
        }
    }

    // Resets the color of the last interacted object's renderers
    private void ResetColor()
    {
        if (lastInteractedObject != null)
        {
            Renderer[] renderers = lastInteractedObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                // Restore the original color only if the material supports _Color
                if (renderer.material.HasProperty("_Color"))
                {
                    renderer.material.color = originalColor;
                }
            }
            lastInteractedObject = null;
        }
    }
}