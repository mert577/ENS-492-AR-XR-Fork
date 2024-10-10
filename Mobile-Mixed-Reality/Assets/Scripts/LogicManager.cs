using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicManager : MonoBehaviour
{

    [SerializeField] GameObject erkaySavas;
    [SerializeField] GameObject gameCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnHoca()
    {
        // Get the forward direction of the camera
        Vector3 cameraForward = gameCamera.transform.forward;

        // Ignore the y component
        Vector3 cameraForwardXZ = new Vector3(cameraForward.x, 0f, cameraForward.z).normalized;

        // Now you have the dominant x-z direction
        Debug.Log("Dominant x-z direction: " + cameraForwardXZ);

        GameObject erkaySavasNew = Instantiate(erkaySavas, gameCamera.transform.position + (cameraForwardXZ * 2), Quaternion.identity);
        erkaySavasNew.transform.position = new Vector3(erkaySavasNew.transform.position.x, gameCamera.transform.position.y - 1.8f, erkaySavasNew.transform.position.z);
        
        // rotate y by 180
        Transform erkaySavasTransform = erkaySavasNew.transform;
        Quaternion newRotation = Quaternion.Euler(0f, 180f, 0f);
        erkaySavasTransform.rotation = newRotation;

        erkaySavasNew.SetActive(true);
    }
}
