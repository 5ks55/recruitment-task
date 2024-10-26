using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float panSpeed = 20f; // Speed of panning the camera
    [SerializeField] private float rotationSpeed = 100f; // Speed of rotating the camera
    [SerializeField] private Vector2 panLimit; // Limits for panning

    private Vector2 moveInput; // Input for movement
    private float rotationInput; // Input for rotation
    private Vector3 initialPosition; // Initial position of the camera

    private void Start()
    {
        initialPosition = transform.position;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed || context.canceled)
        {
            moveInput = context.ReadValue<Vector2>(); // Read movement input
        }
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        if (context.performed || context.canceled)
        {
            Vector2 rotationValue = context.ReadValue<Vector2>();
            rotationInput = rotationValue.x; // Use only the X axis for rotation
        }
    }

    void Update()
    {
        HandleMovementAndRotation();
    }

    private void HandleMovementAndRotation()
    {
        // Get the current position of the camera
        Vector3 pos = transform.position;

        // Get the camera's forward direction
        Vector3 forward = Camera.main.transform.forward;
        forward.y = 0;
        forward.Normalize(); 

        Vector3 right = Camera.main.transform.right;
        right.y = 0;
        right.Normalize(); 

        // Move based on input
        pos += (right * moveInput.x + forward * moveInput.y) * panSpeed * Time.deltaTime;

        // Clamp movement within limits relative to the initial position
        pos.x = Mathf.Clamp(pos.x, initialPosition.x - panLimit.x, initialPosition.x + panLimit.x);
        pos.z = Mathf.Clamp(pos.z, initialPosition.z - panLimit.y, initialPosition.z + panLimit.y);
        transform.position = pos;

        // Handle rotation
        if (rotationInput != 0)
        {
            float rotationAmount = rotationInput * rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(60, transform.rotation.eulerAngles.y + rotationAmount, 0);
        }
    }
}