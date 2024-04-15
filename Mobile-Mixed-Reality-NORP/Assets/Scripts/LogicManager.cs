using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicManager : MonoBehaviour
{

    [SerializeField] GameObject gameCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject SpawnHoca(GameObject hoca)
    {
        // Get the forward direction of the camera
        Vector3 cameraForward = gameCamera.transform.forward;

        // Ignore the y component
        Vector3 cameraForwardXZ = new Vector3(cameraForward.x, 0f, cameraForward.z).normalized;

        // Now you have the dominant x-z direction
        Debug.Log("Dominant x-z direction: " + cameraForwardXZ);

        GameObject hocaNew = Instantiate(hoca, gameCamera.transform.position + (cameraForwardXZ * 1.8f), Quaternion.identity);
        hocaNew.transform.position = new Vector3(hocaNew.transform.position.x, gameCamera.transform.position.y - 1.8f, hocaNew.transform.position.z);
        
        // rotate y by 180
        Transform hocaTransform = hocaNew.transform;
        Quaternion newRotation = Quaternion.Euler(0f, 180f, 0f);
        hocaTransform.rotation = newRotation;

        hocaNew.SetActive(true);

        return hocaNew;
    }

    public void AttachAudio(GameObject hoca, GameObject hocaAudio)
    {
        hocaAudio.transform.SetParent(hoca.transform);
        hocaAudio.transform.localPosition = Vector3.zero;
        hocaAudio.GetComponent<AudioSource>().Stop();
        hocaAudio.GetComponent<AudioSource>().Play();
    }

    public void GoToSabanciIntroScene()
    {
        SceneManager.LoadScene("SabanciIntro");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
