using UnityEngine;

public class RandomChestMover : MonoBehaviour
{
    // Coordinates defining the boundaries for chest movement
    [SerializeField] private Vector3 minBound = new Vector3(-11.25f, 1.489621f, 14.78f);  // Top-left corner
    [SerializeField] private Vector3 maxBound = new Vector3(0.47f, 1.489621f, 2.72f);    // Bottom-right corner

    void Start()
    {
        // Move the chest to a random position and set its rotation
        MoveChest();
    }

    void MoveChest()
    {
        // Generate a random position within the specified bounds
        Vector3 randomPosition = new Vector3(
            Random.Range(minBound.x, maxBound.x),
            Random.Range(minBound.y, maxBound.y),
            Random.Range(minBound.z, maxBound.z)
        );

        // Calculate the center point of the area
        Vector3 centerPoint = (minBound + maxBound) / 2;

        // Direction vector from the random position to the center
        Vector3 directionToCenter = centerPoint - randomPosition;

        // Determine rotation to make the chest face towards the center
        Quaternion chestRotation = Quaternion.LookRotation(directionToCenter);

        // Update the chest's position and rotation
        transform.position = randomPosition;
        transform.rotation = chestRotation;
    }
}