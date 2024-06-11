using UnityEngine;

public class LookAtCameraDirect : MonoBehaviour
{
    // [SerializeField] float adjustRotationY = 1;
    private Transform mainCameraTransform;

    private void Start()
    {
        // Find the MainCamera by its tag
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        // Get the Transform component of the MainCamera
        mainCameraTransform = mainCamera.transform;

    }

    private void Update()
    {

        // Calculate the direction to the camera
        Vector3 directionToCamera = mainCameraTransform.position - transform.position;

        // Set the rotation to look at the camera along the y-axis
        transform.rotation = Quaternion.LookRotation(directionToCamera);
    }
}
