using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskScript : MonoBehaviour
{
    public Vector3 targetPosition;
    public float speed = 2f;
    public string color;

    private AdvancedGameManager advancedGameManager;


    private void Start()
    {
        advancedGameManager = FindObjectOfType<AdvancedGameManager>();
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 2f)
        {
            if(color == "red")
            {
                advancedGameManager.DestroyFirstRedDisk(true);
            }else if(color == "green")
            {
                advancedGameManager.DestroyFirstGreenDisk(true);
            }else if (color == "blue")
            {
                advancedGameManager.DestroyFirstBlueDisk(true);
            }
        }
    }
}
