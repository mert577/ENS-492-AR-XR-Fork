using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowCamera : MonoBehaviour
{
    [SerializeField] GameObject gameCamera;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = gameCamera.transform.position;
    }
}
