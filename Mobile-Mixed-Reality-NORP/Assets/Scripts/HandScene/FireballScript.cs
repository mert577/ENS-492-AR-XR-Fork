using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour
{
    // The time in seconds before the object disappears
    [SerializeField] float timeToDisappear = 10f;

    void Start()
    {
        // Start the coroutine to make the object disappear after the specified time
        StartCoroutine(Disappear());
    }

    IEnumerator Disappear()
    {
        // Wait for the specified time
        yield return new WaitForSeconds(timeToDisappear);

        // Deactivate the game object
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy")) {
            Destroy(other);
            Destroy(gameObject);
        }
    }
}
