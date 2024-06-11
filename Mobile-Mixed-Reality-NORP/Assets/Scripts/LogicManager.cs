using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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

        GameObject hocaNew = Instantiate(hoca, gameCamera.transform.position + (cameraForwardXZ * 2f), Quaternion.identity);
        hocaNew.transform.position = new Vector3(hocaNew.transform.position.x, gameCamera.transform.position.y - 1.8f, hocaNew.transform.position.z);
        
        // rotate y by 180
        Transform hocaTransform = hocaNew.transform;
        Quaternion newRotation = Quaternion.Euler(0f, 180f, 0f);
        hocaTransform.rotation = newRotation;

        hocaNew.SetActive(true);

        return hocaNew;
    }

    public void AttachAudio(GameObject hoca, GameObject Audio)
    {
        Audio.transform.SetParent(hoca.transform);
        Audio.transform.localPosition = Vector3.zero;
        Audio.GetComponent<AudioSource>().Stop();
        Audio.GetComponent<AudioSource>().Play();
    }


    public void GoToSabanciIntroScene()
    {
        SceneManager.LoadScene("SabanciIntro");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToLocationScene()
    {
        SceneManager.LoadScene("SabanciLocation");
    }

    public void GoToHandScene()
    {
        SceneManager.LoadScene("ExampleInteraction");
    }

    public void GoToOpenCVScene()
    {
        SceneManager.LoadScene("HandScene");
    }




    public void TextPopUp(GameObject prefab, string msg)
    {
        Vector3 cameraForward = gameCamera.transform.forward;
        Vector3 cameraForwardXZ = new Vector3(cameraForward.x, 0f, cameraForward.z).normalized;
        Vector3 newPosition = gameCamera.transform.position + (cameraForwardXZ * 3f);

        GameObject newTextObject = Instantiate(prefab, newPosition, Quaternion.identity);
        TextMeshPro textMesh = newTextObject.GetComponent<TextMeshPro>();

        if (textMesh != null)
        {
            textMesh.text = msg;
            // textMesh.font = fontAsset;
            StartCoroutine(ShowAndDisappearText(textMesh));
        }
        else
        {
            Debug.LogError("No TextMeshPro component found on the provided prefab.");
            Destroy(newTextObject);
        }
    }

    IEnumerator ShowAndDisappearText(TextMeshPro textMesh)
    {
        // Initial scale of the text
        Vector3 initialScale = textMesh.transform.localScale;
        // Scale for the "glow up" effect
        Vector3 glowUpScale = initialScale * 1.5f;

        // Show the text with a glow up effect
        float timer = 0f;
        while (timer < 1f) // Adjust duration of the glow up effect as needed
        {
            textMesh.transform.localScale = Vector3.Lerp(initialScale, glowUpScale, timer / 0.5f);
            timer += Time.deltaTime;
            yield return null;
        }

        // Reset scale
        textMesh.transform.localScale = initialScale;

        // Wait for a short duration
        yield return new WaitForSeconds(1f); // Adjust the duration the text stays visible

        // Fade out effect
        timer = 0f;
        Color initialColor = textMesh.color;
        Color transparentColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);
        while (timer < 1f) // Adjust duration of the fade out effect as needed
        {
            textMesh.color = Color.Lerp(initialColor, transparentColor, timer);
            timer += Time.deltaTime;
            yield return null;
        }

        // Destroy the text object after it disappears
        Destroy(textMesh.gameObject);
    }
}
