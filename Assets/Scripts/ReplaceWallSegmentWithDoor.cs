using UnityEngine;

public class ReplaceWallSegmentWithDoor : MonoBehaviour
{
    [SerializeField] private GameObject[] wallSegments; // Array of wall segments

    void Start()
    {
        // Randomly position the first wall segment
        RandomizeFirstSegmentPosition();
    }

    void RandomizeFirstSegmentPosition()
    {
        // Exit if there is only one or no wall segments
        if (wallSegments.Length <= 1) return;

        // Generate a random index for swapping
        int randomIndex = Random.Range(0, wallSegments.Length);

        // No need to swap if the random index is 0
        if (randomIndex == 0) return;

        // Store the position and rotation of the first wall segment
        Vector3 firstSegmentPosition = wallSegments[0].transform.position;
        Quaternion firstSegmentRotation = wallSegments[0].transform.rotation;

        // Swap the position and rotation of the first segment with the randomly selected one
        wallSegments[0].transform.position = wallSegments[randomIndex].transform.position;
        wallSegments[0].transform.rotation = wallSegments[randomIndex].transform.rotation;

        wallSegments[randomIndex].transform.position = firstSegmentPosition;
        wallSegments[randomIndex].transform.rotation = firstSegmentRotation;
    }
}