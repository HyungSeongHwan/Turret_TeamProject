using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSetup : MonoBehaviour
{
    private void Start()
    {
        Initialize();
    }


    public void Initialize()
    {
        CinemachineVirtualCamera followCam = FindObjectOfType<CinemachineVirtualCamera>();

        followCam.Follow = transform;
        followCam.LookAt = transform;
    }
}
