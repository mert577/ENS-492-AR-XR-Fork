using UnityEngine;

public class FollowObjectWithDifferentY : MonoBehaviour
{
    public Vector3 normalPositionOffset;
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

    void Update()
    {
        Vector3 rotatedOffset = mainCameraTransform.rotation * normalPositionOffset;
        //transform.position = parent.transform.position + rotatedOffset;



        float float1 = rotatedOffset.x;
        float float2 = rotatedOffset.z;

        Vector2 vector1 = new Vector2(float1, float2);

        Vector2 normalizedVector = vector1.normalized;

        float scaleFactor = 5f; // Desired Euclidean distance
        Vector2 scaledVector = normalizedVector * scaleFactor;

        float scaledFloat1 = scaledVector.x;
        float scaledFloat2 = scaledVector.y;




        Vector3 newPosition = new Vector3(scaledFloat1 + mainCameraTransform.transform.position.x,
                                  mainCameraTransform.position.y - 1.7f,
                                  scaledFloat2 + mainCameraTransform.transform.position.z);
        transform.position = newPosition;

    }
}
