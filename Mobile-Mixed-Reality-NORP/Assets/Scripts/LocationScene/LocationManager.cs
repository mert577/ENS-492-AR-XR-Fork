using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_ANDROID
using UnityEngine.Android;
#endif




public class LocationManager : MonoBehaviour
{

    [SerializeField] GameObject PopUpText;

    [SerializeField] float locationThreshold = 0.0003f;

    // location based --> cafeteria
    [SerializeField] float cafeteriaLatitude = 40.8913894f;
    [SerializeField] float cafeteriaLongitude = 29.3798876f;
    [SerializeField] List<GameObject> cafeteriaCollectables;

    // location based --> IC
    [SerializeField] float ICLatitude = 40.89021f;
    [SerializeField] float ICLongitude = 29.37744f;
    [SerializeField] List<GameObject> ICCollectables;

    // location based --> grass
    [SerializeField] float grassLatitude = 40.8913442f;
    [SerializeField] float grassLongitude = 29.3791577f;
    [SerializeField] List<GameObject> grassCollectables;

    // location based --> FENS
    [SerializeField] float FENSLatitude = 40.8906637f;
    [SerializeField] float FENSLongitude = 29.3797448f;
    [SerializeField] List<GameObject> FENSCollectables;

    // location based --> FMAN
    [SerializeField] float FMANLatitude = 40.8920220f;
    [SerializeField] float FMANLongitude = 29.3790876f;

    private LogicManager logicManager;

    // Start is called before the first frame update
    void Start()
    {
        logicManager = FindObjectOfType<LogicManager>();

        #if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
        #endif

        if (/*!Input.location.isEnabledByUser*/true)
        {
            logicManager.TextPopUp(PopUpText, "Location is not enabled!!!");
        }
        else
        {
            Input.location.Start();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
