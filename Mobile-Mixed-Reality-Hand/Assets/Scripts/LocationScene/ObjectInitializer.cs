using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class ObjectInitializer : MonoBehaviour
{
    [SerializeField] GameObject gameCamera;

    private void Start()
    {
        // InitializeObjectsInCircle();
    }

    public void InitializeObjectsInCircle(List<GameObject> objectsToInitialize)
    {
        int objectCount = objectsToInitialize.Count;
        float angleStep = 360f / objectCount;
        float radius = 4f;

        for (int i = 0; i < objectCount; i++)
        {
            float angle = i * angleStep;
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            float z = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            Vector3 spawnPosition = new Vector3(x, gameCamera.transform.position.y - Random.Range(0.4f, 0.7f), z) + gameCamera.transform.position;

            GameObject obj = Instantiate(objectsToInitialize[i], spawnPosition, Quaternion.identity);

            obj.transform.localScale = Vector3.zero;
            obj.transform.DOScale(Vector3.one, 4f).SetEase(Ease.OutElastic);
        }
    }

    public void DestroyObjectsWithTag(string tag)
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);

        foreach (GameObject obj in objectsWithTag)
        {
            Destroy(obj);
        }
    }
}
