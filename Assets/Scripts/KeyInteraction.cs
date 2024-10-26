using UnityEngine;
using UnityEngine.InputSystem;

public class KeyInteraction : Sounds
{
    [SerializeField] private GameObject questionWindow; // Reference to the question window
    [SerializeField] private GameObject keyIcon; // Reference to the key icon in the inventory
    [SerializeField] private PlayerInput playerInput; // Reference to the player's PlayerInput
    [SerializeField] private LayerMask interactableLayer; // Layer for interactive objects

    private Camera mainCamera;
    private InputAction mouseClickAction;

    private void Awake()
    {
        mainCamera = Camera.main; 

        if (playerInput != null)
        {
            mouseClickAction = playerInput.actions["MouseClick"]; 
        }

        questionWindow.SetActive(false);
        keyIcon.SetActive(false);
    }

    private void Update()
    {
        // If the key has already been picked up (icon is active), do nothing
        if (mouseClickAction == null || keyIcon.activeSelf)
            return;

        // Check if the mouse click action was triggered
        if (mouseClickAction.triggered)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = mainCamera.ScreenPointToRay(mousePosition);

            // Perform a raycast to detect interactive objects
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, interactableLayer))
            {
                // Check if the hit object is this object
                if (hit.transform == this.transform)
                {
                    questionWindow.SetActive(true);
                }
            }
        }
    }

    // Method for the "Yes" button
    public void OnYesButtonClicked()
    {
        PlaySound(sounds[0]);
        gameObject.SetActive(false);
        keyIcon.SetActive(true);
        questionWindow.SetActive(false);
    }

    // Method for the "No" button
    public void OnNoButtonClicked()
    {
        questionWindow.SetActive(false);
    }

    // Method to check if the key is in the inventory
    public bool HasKey()
    {
        return keyIcon.activeSelf;
    }
}