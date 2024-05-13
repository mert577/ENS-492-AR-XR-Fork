using UnityEngine;

public class SpinOnYAxis : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90f;

    private void Update()
    {
        // Rotate the object around the y-axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
