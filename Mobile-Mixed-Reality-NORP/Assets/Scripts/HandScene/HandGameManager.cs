using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class HandGameManager : MonoBehaviour
{

    private ManoGestureContinuous grab;
    private ManoGestureContinuous pinch;
    private ManoGestureTrigger click;
    private ManoGestureTrigger pick;
    private Vector3 trackedPosition;
    private GestureInfo gesture;
    private bool continousGestureStarted = false;
    private GameObject newFireBallCts;


    [SerializeField] GameObject fireBall;
    [SerializeField] GameObject enemy;

    [SerializeField] float enemySpawnDistance;
    [SerializeField] float fireballSpeed;

    [SerializeField] Text infoText;

    private Transform mainCameraTransform;
    private LogicManager logicManager;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        double current = (double)ManomotionManager.Instance.Mano_License.days_left;
        // Debug.Log("ok: " + current);
        infoText.text = current.ToString();

        logicManager = FindObjectOfType<LogicManager>();
        grab = ManoGestureContinuous.CLOSED_HAND_GESTURE;
        pinch = ManoGestureContinuous.HOLD_GESTURE;
        click = ManoGestureTrigger.CLICK;
        pick = ManoGestureTrigger.PICK;

        // Find the MainCamera by its tag
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        mainCameraTransform = mainCamera.transform;

    }

    // Update is called once per frame
    void Update()
    {
        gesture = ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info;

        timer += Time.deltaTime;
        if (gesture.mano_gesture_trigger == click ||
            gesture.mano_gesture_trigger == pick /*|| Input.GetKeyDown(KeyCode.K)*/)
        {
            trackedPosition = ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info.palm_center - new Vector3(0.5f, 0.5f, 0);
            trackedPosition.z = ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info.depth_estimation * 2; // add depth to tracking
                                                                                                                       // now all x y z is arranged
            Vector3 direction = mainCameraTransform.forward;

            Vector3 rotatedTrackedPosition = mainCameraTransform.rotation * trackedPosition;
            Vector3 finalPosition = rotatedTrackedPosition + mainCameraTransform.position;
            infoText.text = trackedPosition.ToString();
            GameObject newFireBall = Instantiate(fireBall, finalPosition/*direction + mainCameraTransform.position*/, mainCameraTransform.rotation);
            newFireBall.GetComponent<Rigidbody>().velocity = direction * fireballSpeed;

        }

        if(timer > 10)
        {
            timer = 0;
            // Get the forward direction of the camera
            Vector3 cameraForward = mainCameraTransform.forward;
            // Ignore the y component
            Vector3 cameraForwardXZ = new Vector3(cameraForward.x, 0.0f, cameraForward.z).normalized;

            Instantiate(enemy, enemySpawnDistance * cameraForwardXZ + mainCameraTransform.position, Quaternion.identity);
        }

        if (gesture.mano_gesture_continuous == grab)
        {
            if (continousGestureStarted)
            {
                newFireBallCts.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
            }
            else
            {
                continousGestureStarted = true;
                Vector3 rotatedTrackedPosition = mainCameraTransform.rotation * trackedPosition;
                Vector3 finalPosition = rotatedTrackedPosition + mainCameraTransform.position;
                infoText.text = trackedPosition.ToString();
                newFireBallCts = Instantiate(fireBall, finalPosition/*direction + mainCameraTransform.position*/, mainCameraTransform.rotation);
            }
            
        }
        else
        {
            if (continousGestureStarted)
            {
                continousGestureStarted = false;
                Vector3 direction = mainCameraTransform.forward;
                newFireBallCts.GetComponent<Rigidbody>().velocity = direction * fireballSpeed;

            }
        }


    }
}
