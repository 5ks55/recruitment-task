using UnityEngine;
using UnityEngine.InputSystem;

public class ChestInteraction : Sounds
{
    [SerializeField] private GameObject questionWindow; // Window asking for confirmation
    [SerializeField] private Animator chestAnimator; // Animator for the chest
    [SerializeField] private GameObject key; // Key object inside the chest
    [SerializeField] private LayerMask interactableLayer; // Layer for interactive objects
    [SerializeField] private PlayerInput playerInput; // Reference to the PlayerInput
    [SerializeField] private int defaultLayer = 0; // Index of the layer after opening (usually 0 - Default)

    private Camera mainCamera; // Reference to the main camera
    private InputAction mouseClickAction; // Input action for mouse clicks
    private bool isChestOpen = false; // Flag to check if the chest is open

    private void Awake()
    {
        mainCamera = Camera.main;

        if (playerInput != null)
        {
            mouseClickAction = playerInput.actions["MouseClick"];
        }

        questionWindow.SetActive(false);
        key.SetActive(false);
    }

    private void Update()
    {
        if (mouseClickAction == null || isChestOpen) // Exit if no input action or the chest is already open
            return;

        if (mouseClickAction.triggered) // Check if the mouse was clicked
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue(); 
            Ray ray = mainCamera.ScreenPointToRay(mousePosition); 

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, interactableLayer)) // Perform raycasting
            {
                // Check if the raycast hit the chest
                if (hit.transform.root == this.transform)
                {
                    questionWindow.SetActive(true);
                }
            }
        }
    }

    // Method for the "Yes" button
    public void OnYesButtonClicked()
    {
        chestAnimator.SetBool("isOpen", true);
        PlaySound(sounds[0]);
        isChestOpen = true;

        key.SetActive(true);
        questionWindow.SetActive(false);

        // Change the layer of the chest and all its children, except the key
        ChangeLayerRecursively(transform, defaultLayer);
    }

    // Recursive method to change the layer
    private void ChangeLayerRecursively(Transform obj, int layer)
    {
        // Skip the object if it is the key
        if (obj.CompareTag("Key"))
            return;

        obj.gameObject.layer = layer;

        // Iterate through all child objects
        foreach (Transform child in obj)
        {
            ChangeLayerRecursively(child, layer); // Recursively change the layer for children
        }
    }

    // Method for the "No" button
    public void OnNoButtonClicked()
    {
        questionWindow.SetActive(false);
    }
}