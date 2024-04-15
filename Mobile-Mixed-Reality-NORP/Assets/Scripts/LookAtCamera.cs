using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform mainCameraTransform;

    private void Start()
    {
        // Find the MainCamera by its tag
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        if (mainCamera != null)
        {
            // Get the Transform component of the MainCamera
            mainCameraTransform = mainCamera.transform;
        }
        else
        {
            Debug.LogWarning("MainCamera not found! Make sure it is tagged as 'MainCamera'.");
        }
    }

    private void Update()
    {
        // Check if the MainCamera's transform has been found
        if (mainCameraTransform != null)
        {
            // Calculate the direction to the camera
            Vector3 directionToCamera = mainCameraTransform.position - transform.position;

            // Set the rotation to look at the camera along the y-axis
            transform.rotation = Quaternion.LookRotation(new Vector3(directionToCamera.x, 0, directionToCamera.z));
        }
    }
}
