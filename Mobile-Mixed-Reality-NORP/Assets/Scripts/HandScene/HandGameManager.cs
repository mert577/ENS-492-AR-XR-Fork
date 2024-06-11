using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGameManager : MonoBehaviour
{

    private ManoGestureContinuous grab;
    private ManoGestureContinuous pinch;
    private ManoGestureTrigger click;

    [SerializeField] GameObject fireBall;
    [SerializeField] GameObject enemy;

    [SerializeField] float enemySpawnDistance;


    private Transform mainCameraTransform;
    private LogicManager logicManager;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        logicManager = FindObjectOfType<LogicManager>();
        grab = ManoGestureContinuous.CLOSED_HAND_GESTURE;
        pinch = ManoGestureContinuous.HOLD_GESTURE;
        click = ManoGestureTrigger.CLICK;

        // Find the MainCamera by its tag
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        mainCameraTransform = mainCamera.transform;

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_trigger == click)
        {
            Vector3 direction = mainCameraTransform.forward;
            Instantiate(fireBall, direction + mainCameraTransform.position, mainCameraTransform.rotation);
            fireBall.GetComponent<Rigidbody>().velocity = direction;
        }

        if(timer > 5)
        {
            timer = 0;
            // Get the forward direction of the camera
            Vector3 cameraForward = mainCameraTransform.forward;
            // Ignore the y component
            Vector3 cameraForwardXZ = new Vector3(cameraForward.x, 0.5f, cameraForward.z).normalized;

            Instantiate(enemy, enemySpawnDistance * cameraForwardXZ + mainCameraTransform.position, Quaternion.identity);
        }
    }
}
