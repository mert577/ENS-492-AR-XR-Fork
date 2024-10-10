using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookInteraction : MonoBehaviour
{

    [SerializeField] float raycastDistance;
    [SerializeField] GameObject gameCamera;
    [SerializeField] GameObject loadingMask;

    private LogicManager logicManager;
    private bool isHittingSpawnHoca1 = false;
    private float timeElapsed = 0f;
    private float requiredTime = 3f;

    // Start is called before the first frame update
    void Start()
    {
        logicManager = FindObjectOfType<LogicManager>();
    }

    // Update is called once per frame
    void Update()
    {
        InteractByLooking();
    }

    private void InteractByLooking()
    {
        // Find the middle point of the camera's viewport
        Vector3 viewportCenter = new Vector3(0.5f, 0.5f, 0f);

        Ray ray = gameCamera.GetComponent<Camera>().ViewportPointToRay(viewportCenter);

        // Declare a variable to store the RaycastHit
        RaycastHit hit;

        // Check if something is hit by the ray
        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
            GameObject collidedUI = hit.collider.gameObject;

            // Do something with the hit point or object
            Debug.Log("Hit something at: " + collidedUI.name);

            if (collidedUI.name == "SpawnHoca1")
            {
                loadingMask.SetActive(true);
                loadingMask.transform.SetParent(collidedUI.transform, false);

                isHittingSpawnHoca1 = true;
                timeElapsed += Time.deltaTime;

                loadingMask.transform.localScale = Vector3.Lerp(new Vector3(0.95f, 0.95f, 0.95f), new Vector3(0f, 0f, 0f), timeElapsed / requiredTime);

                if (timeElapsed >= requiredTime)
                {
                    AdjustLoadingMaskBack(loadingMask);

                    logicManager.SpawnHoca();
                    // Reset timer and flag
                    timeElapsed = 0f;
                    isHittingSpawnHoca1 = false;
                }
            }
            else
            {
                AdjustLoadingMaskBack(loadingMask);
                // Reset timer and flag if not hitting SpawnHoca1
                timeElapsed = 0f;
                isHittingSpawnHoca1 = false;
            }
        }
        else
        {
            AdjustLoadingMaskBack(loadingMask);
            // Reset timer and flag if not hitting anything
            timeElapsed = 0f;
            isHittingSpawnHoca1 = false;
        }

        // Draw the ray in the scene view for debugging
        Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.red);
    }

    private void AdjustLoadingMaskBack(GameObject spriteMask)
    {
        spriteMask.transform.localScale = new Vector3(0.95f, 0.95f, 0.95f);
        spriteMask.SetActive(false);
    }

}
