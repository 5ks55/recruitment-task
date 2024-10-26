using UnityEngine;
using UnityEngine.InputSystem;

public class DoorInteraction : Sounds
{
    [SerializeField] private GameObject keyIcon; // Reference to the key icon in the inventory
    [SerializeField] private GameObject needKeyMessage; // Message saying "You need a key!"
    [SerializeField] private GameObject questionWindow; // Window asking "Open?"
    [SerializeField] private Animator doorAnimator; // Animator for the door
    [SerializeField] private PlayerInput playerInput; // Reference to the player's PlayerInput
    [SerializeField] private string doorOpenAnimationTrigger = "OpenDoor"; // Trigger for the door open animation

    private InputAction mouseClickAction; // Input action for mouse clicks
    private Camera mainCamera; // Reference to the main camera

    private void Awake()
    {
        mainCamera = Camera.main; 

        if (playerInput != null)
        {
            mouseClickAction = playerInput.actions["MouseClick"];
        }

        needKeyMessage.SetActive(false);
        questionWindow.SetActive(false);
    }

    private void Update()
    {
        if (mouseClickAction == null)
            return;

        if (mouseClickAction.triggered) // Check if the mouse was clicked
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue(); 
            Ray ray = mainCamera.ScreenPointToRay(mousePosition); 

            if (Physics.Raycast(ray, out RaycastHit hit)) // Perform raycasting
            {
                // Check if the raycast hit this door
                if (hit.transform == this.transform)
                {
                    if (keyIcon.activeSelf)
                    {
                        questionWindow.SetActive(true);
                    }
                    else
                    {
                        PlaySound(sounds[0]);
                        needKeyMessage.SetActive(true);
                    }
                }
            }
        }
    }

    // Method to hide the "You need a key!" message
    public void HideNeedKeyMessage()
    {
        needKeyMessage.SetActive(false); 
    }

    // Method for the "Yes" button in the question window
    public void OnYesButtonClicked()
    {
        doorAnimator.SetTrigger(doorOpenAnimationTrigger);

        PlaySound(sounds[1]);

        keyIcon.SetActive(false);

        questionWindow.SetActive(false);

        FindObjectOfType<Timer>().EndGame();
    }

    // Method for the "No" button in the question window
    public void OnNoButtonClicked()
    {
        questionWindow.SetActive(false);
    }
}