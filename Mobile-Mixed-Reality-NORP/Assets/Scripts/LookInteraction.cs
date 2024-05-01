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
    [SerializeField] AudioClip popSoundClip;

    [SerializeField] float raycastDistance;
    [SerializeField] GameObject gameCamera;
    [SerializeField] GameObject loadingMask;

    private LogicManager logicManager;
    private float timeElapsed = 0f;
    [SerializeField] float requiredTime;


    private GameObject ErkayHocaScene;
    private GameObject KursatHocaScene;


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


        // Perform raycast and store hits for objects with the "Interactable" tag
        RaycastHit[] allHits = Physics.RaycastAll(ray, raycastDistance);

        if (allHits.Length <= 0)
        {
            AdjustLoadingMaskBack(loadingMask);
        }

        foreach (RaycastHit hit in allHits)
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                // Add the hit to the list
                hits.Add(hit);
            }
        }




        // Check if something is hit by the ray
        foreach (RaycastHit hit in hits)
        {
            GameObject collidedUI = hit.collider.gameObject;

            Debug.Log("Hit something at: " + collidedUI.name);

            InteractableObject interactableObject = collidedUI.GetComponent<InteractableObject>();

            switch (interactableObject.interactableType)
            {
                case InteractableType.SpawnErkayHoca:
                    StartLookInteraction(collidedUI);
                    if (timeElapsed >= requiredTime)
                    {
                        ErkayHocaScene = logicManager.SpawnHoca(erkaySavas);
                        audioSource.GetComponent<AudioSource>().clip = erkaySavasClip;
                        logicManager.AttachAudio(ErkayHocaScene, audioSource);
                        AdjustLoadingMaskBack(loadingMask);
                    }
                    break;

                case InteractableType.SpawnKursatHoca:
                    StartLookInteraction(collidedUI);
                    if (timeElapsed >= requiredTime)
                    {
                        KursatHocaScene = logicManager.SpawnHoca(kursatHoca);
                        audioSource.GetComponent<AudioSource>().clip = kursatHocaClip;
                        logicManager.AttachAudio(KursatHocaScene, audioSource);
                        AdjustLoadingMaskBack(loadingMask);
                    }
                    break;

                case InteractableType.SabanciIntroButton:
                    StartLookInteraction(collidedUI);
                    if (timeElapsed >= requiredTime)
                    {
                        logicManager.GoToSabanciIntroScene();
                        AdjustLoadingMaskBack(loadingMask);
                    }
                    break;

                case InteractableType.MainMenuButton:
                    StartLookInteraction(collidedUI);
                    if (timeElapsed >= requiredTime)
                    {
                        logicManager.GoToMainMenu();
                        AdjustLoadingMaskBack(loadingMask);
                    }
                    break;

                case InteractableType.ErkayHocaDialog1:
                    StartLookInteraction(collidedUI);
                    if (timeElapsed >= requiredTime)
                    {
                        audioSource.GetComponent<AudioSource>().clip = kursatHocaClip;
                        logicManager.AttachAudio(ErkayHocaScene, audioSource);
                        AdjustLoadingMaskBack(loadingMask);
                    }
                    break;

                case InteractableType.ErkayHocaDialog2:
                    StartLookInteraction(collidedUI);
                    if (timeElapsed >= requiredTime)
                    {
                        audioSource.GetComponent<AudioSource>().clip = erkaySavasClip;
                        logicManager.AttachAudio(ErkayHocaScene, audioSource);
                        AdjustLoadingMaskBack(loadingMask);
                    }
                    break;

                case InteractableType.ErkayHocaDialog3:
                    StartLookInteraction(collidedUI);
                    if (timeElapsed >= requiredTime)
                    {
                        audioSource.GetComponent<AudioSource>().clip = popSoundClip;
                        logicManager.AttachAudio(ErkayHocaScene, audioSource);
                        AdjustLoadingMaskBack(loadingMask);
                    }
                    break;

                case InteractableType.KursatHocaDialog1:
                    StartLookInteraction(collidedUI);
                    if (timeElapsed >= requiredTime)
                    {
                        audioSource.GetComponent<AudioSource>().clip = erkaySavasClip;
                        logicManager.AttachAudio(KursatHocaScene, audioSource);
                        AdjustLoadingMaskBack(loadingMask);
                    }
                    break;

                case InteractableType.KursatHocaDialog2:
                    StartLookInteraction(collidedUI);
                    if (timeElapsed >= requiredTime)
                    {
                        audioSource.GetComponent<AudioSource>().clip = kursatHocaClip;
                        logicManager.AttachAudio(KursatHocaScene, audioSource);
                        AdjustLoadingMaskBack(loadingMask);
                    }
                    break;

                case InteractableType.KursatHocaDialog3:
                    StartLookInteraction(collidedUI);
                    if (timeElapsed >= requiredTime)
                    {
                        audioSource.GetComponent<AudioSource>().clip = popSoundClip;
                        logicManager.AttachAudio(KursatHocaScene, audioSource);
                        AdjustLoadingMaskBack(loadingMask);
                    }
                    break;

                default:
                    AdjustLoadingMaskBack(loadingMask);
                    break;
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
