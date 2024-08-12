using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using Unity.VisualScripting;

public class AdvancedGameManager : MonoBehaviour
{
    private ManoGestureContinuous grab;
    private ManoGestureContinuous pinch;
    private ManoGestureTrigger click;
    private ManoGestureTrigger pick;
    private GestureInfo gesture;

    public float spawnRate;
    public TextMeshPro healthText;

    public GameObject blueDiskPrefab;
    public GameObject greenDiskPrefab;
    public GameObject redDiskPrefab;

    public GameObject bonusDiskPrefab;


    private List<GameObject> greenDisks = new List<GameObject>();
    private List<GameObject> redDisks = new List<GameObject>();
    private List<GameObject> blueDisks = new List<GameObject>();

    private List<GameObject> bonusDisk = new List<GameObject>();


    public Transform mainCameraTransform;

    private Vector3 greenPosition;
    private Vector3 redPosition;
    private Vector3 bluePosition;

    private Vector3 cameraPosition;
    private Vector3 cameraForward;
    private Vector3 cameraRight;

    private int score;
    private bool gameOver = false;
    void Start()
    {
        cameraPosition = mainCameraTransform.position;
        cameraPosition -= new Vector3 (0, 0.5f, 0);
        cameraForward = mainCameraTransform.forward;
        cameraRight = mainCameraTransform.right;

        greenPosition = cameraPosition + cameraForward * 25f - new Vector3(0, 1f, 0);
        redPosition = greenPosition - cameraRight * 3f;
        bluePosition = greenPosition + cameraRight * 3f;

        grab = ManoGestureContinuous.CLOSED_HAND_GESTURE;
        pinch = ManoGestureContinuous.HOLD_GESTURE;
        click = ManoGestureTrigger.CLICK;
        pick = ManoGestureTrigger.PICK;

        InvokeRepeating("InstantiateRandomDisk", 0f, spawnRate);
        InvokeRepeating("InstantiateBonus", 0f, 10f);

    }

    private void Update()
    {
        gesture = ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info;

        if (gesture.mano_gesture_trigger == click || gesture.mano_gesture_trigger == pick 
            || gesture.mano_gesture_continuous == grab /*|| Input.GetKeyDown(KeyCode.L)*/)
        {
            if (bonusDisk.Count > 0)
            {
                int health;
                if (int.TryParse(healthText.text, out health))
                {
                    health += 1;
                    healthText.text = health.ToString();
                    Debug.Log("Health increased to: " + health);
                }

                GameObject diskk = bonusDisk[0];
                bonusDisk.RemoveAt(0);
                Destroy(diskk);
            }

        }
    }

    void InstantiateBonus()
    {
        if (bonusDisk.Count <= 0)
        {
            GameObject newDisk = Instantiate(bonusDiskPrefab, greenPosition + new Vector3(0, -1f, 0), Quaternion.identity);
            bonusDisk.Add(newDisk);
        }
    }

    void InstantiateRandomDisk()
    {

        int randomDisk = Random.Range(0, 3);
        GameObject newDisk = null;
        Vector3 targetPosition = Vector3.zero;

        switch (randomDisk)
        {
            case 0:
                newDisk = Instantiate(greenDiskPrefab, greenPosition, Quaternion.identity);
                targetPosition = cameraPosition;
                greenDisks.Add(newDisk);
                break;
            case 1:
                newDisk = Instantiate(redDiskPrefab, redPosition, Quaternion.identity);
                targetPosition = cameraPosition - cameraRight * 3f;
                redDisks.Add(newDisk);
                break;
            case 2:
                newDisk = Instantiate(blueDiskPrefab, bluePosition, Quaternion.identity);
                targetPosition = cameraPosition + cameraRight * 3f;
                blueDisks.Add(newDisk);
                break;
        }

        if (newDisk != null)
        {
            DiskScript diskMovement = newDisk.GetComponent<DiskScript>();
            if (diskMovement != null)
            {
                diskMovement.targetPosition = targetPosition;
            }
        }
    }

    public void DestroyFirstGreenDisk(bool isDamage)
    {
        DestroyFirstDisk(greenDisks);
        if (isDamage && !gameOver)
        {
            int health;
            if (int.TryParse(healthText.text, out health))
            {
                health -= 1;
                healthText.text = health.ToString();
                Debug.Log("Health decreased to: " + health);
            }
            else
            {
                Debug.LogWarning("Health text is not a valid integer.");
            }

            if (health <= 0)
            {
                healthText.text = "Game Over! Your score is " + score.ToString();
                gameOver = true;
            }
        }
    }

    public void DestroyFirstRedDisk(bool isDamage)
    {
        DestroyFirstDisk(redDisks);
        if (isDamage && !gameOver)
        {
            int health;
            if (int.TryParse(healthText.text, out health))
            {
                health -= 1;
                healthText.text = health.ToString();
                Debug.Log("Health decreased to: " + health);
            }
            else
            {
                Debug.LogWarning("Health text is not a valid integer.");
            }

            if (health <= 0)
            {
                healthText.text = "Game Over! Your score is " + score.ToString();
                gameOver = true;
            }
        }
    }

    public void DestroyFirstBlueDisk(bool isDamage)
    {
        DestroyFirstDisk(blueDisks);
        if (isDamage && !gameOver)
        {
            int health;
            if (int.TryParse(healthText.text, out health))
            {
                health -= 1;
                healthText.text = health.ToString();
                Debug.Log("Health decreased to: " + health);
            }
            else
            {
                Debug.LogWarning("Health text is not a valid integer.");
            }

            if (health <= 0)
            {
                healthText.text = "Game Over! Your score is " + score.ToString();
                gameOver = true;
            }
        }
    }

    private void DestroyFirstDisk(List<GameObject> diskList)
    {
        if (diskList.Count > 0)
        {
            score++;
            GameObject disk = diskList[0];
            diskList.RemoveAt(0);
            Destroy(disk);
        }
    }

    public void ToggleMusic(GameObject mainMusic)
    {
        if (mainMusic != null)
        {
            AudioSource audioSource = mainMusic.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                    Debug.Log("Music stopped.");
                }
                else
                {
                    audioSource.Play();
                    Debug.Log("Music playing.");
                }
            }
            else
            {
                Debug.LogWarning("No AudioSource component found on MainMusic.");
            }
        }
        else
        {
            Debug.LogWarning("MainMusic GameObject is null.");
        }
    }
}
