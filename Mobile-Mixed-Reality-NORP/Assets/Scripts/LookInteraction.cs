using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookInteraction : MonoBehaviour
{
    [SerializeField] GameObject erkaySavas;
    [SerializeField] GameObject kursatHoca;

    [SerializeField] GameObject audioSource;

    [SerializeField] AudioClip erkaySavasClip;
    [SerializeField] AudioClip kursatHocaClip;

    [SerializeField] float raycastDistance;
    [SerializeField] GameObject gameCamera;
    [SerializeField] GameObject loadingMask;

    private LogicManager logicManager;
    private float timeElapsed = 0f;
    [SerializeField] float requiredTime;

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
        Vector3 viewportCenter = new Vector3(0.5f, 0.5f, 0f);

        Ray ray = gameCamera.GetComponent<Camera>().ViewportPointToRay(viewportCenter);

        // Declare a list to store all hits
        List<RaycastHit> hits = new List<RaycastHit>();

        // Check if something is hit by the ray and store all hits
        RaycastHit[] allHits = Physics.RaycastAll(ray, raycastDistance);
        if (allHits.Length > 0)
        {
            hits.AddRange(allHits);
        }
        else
        {
            AdjustLoadingMaskBack(loadingMask);
        }


        // Check if something is hit by the ray
        foreach (RaycastHit hit in hits)
        {
            GameObject collidedUI = hit.collider.gameObject;

            Debug.Log("Hit something at: " + collidedUI.name);

            if (collidedUI.name == "SpawnErkayHoca")
            {
                StartLookInteraction(collidedUI);

                if (timeElapsed >= requiredTime)
                {
                    GameObject hocaNew = logicManager.SpawnHoca(erkaySavas);
                    audioSource.GetComponent<AudioSource>().clip = erkaySavasClip;
                    logicManager.AttachAudio(hocaNew, audioSource);
                    AdjustLoadingMaskBack(loadingMask);
                }
            }else if (collidedUI.name == "SpawnKursatHoca")
            {
                StartLookInteraction(collidedUI);

                if (timeElapsed >= requiredTime)
                {
                    GameObject hocaNew = logicManager.SpawnHoca(kursatHoca);
                    audioSource.GetComponent<AudioSource>().clip = kursatHocaClip;
                    logicManager.AttachAudio(hocaNew, audioSource);
                    AdjustLoadingMaskBack(loadingMask);
                }
            }
            else if (collidedUI.name == "SabanciIntroButton")
            {
                StartLookInteraction(collidedUI);

                if (timeElapsed >= requiredTime)
                {
                    logicManager.GoToSabanciIntroScene();
                    AdjustLoadingMaskBack(loadingMask);
                }
            }
            else if (collidedUI.name == "MainMenuButton")
            {
                StartLookInteraction(collidedUI);

                if (timeElapsed >= requiredTime)
                {
                    logicManager.GoToMainMenu();
                    AdjustLoadingMaskBack(loadingMask);
                }
            }
            else
            {
                AdjustLoadingMaskBack(loadingMask);

            }
        }


        // Draw the ray in the scene view for debugging
        Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.red);
    }

    private void AdjustLoadingMaskBack(GameObject spriteMask)
    {
        timeElapsed = 0f;
        spriteMask.transform.localScale = new Vector3(0.95f, 0.95f, 0.95f);
        spriteMask.SetActive(false);
    }


    private void StartLookInteraction(GameObject collidedUI)
    {
        loadingMask.SetActive(true);
        loadingMask.transform.SetParent(collidedUI.transform, false);

        timeElapsed += Time.deltaTime;

        loadingMask.transform.localScale = Vector3.Lerp(new Vector3(0.95f, 0.95f, 0.95f), new Vector3(0f, 0f, 0f), timeElapsed / requiredTime);
    }
}
