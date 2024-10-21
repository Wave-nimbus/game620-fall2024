using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Manager_Cam : MonoBehaviour
{
    [SerializeField]
    Camera[] shopCams;
    [SerializeField]
    Camera resCam;

    int camIdx;

    public event Action<int> CamUpdate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camIdx = 0;
        shopCams[0].enabled = true;
        for (int i = 1; i < shopCams.Length; i++)
        {
            shopCams[i].enabled = false;
        }
        resCam.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchCam();
            CamUpdate?.Invoke(camIdx);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchCam(0);
            CamUpdate?.Invoke(camIdx);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchCam(1);
            CamUpdate?.Invoke(camIdx);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchCam(2);
            CamUpdate?.Invoke(camIdx);
        }
    }

    public void ResultCam()
    {
        resCam.enabled = true;
        for (int i = 0; i < shopCams.Length; i++)
        {
            shopCams[i].enabled = false;
        }
    }

    void SwitchCam()
    {
        shopCams[camIdx].enabled = false;
        camIdx = (camIdx + 1) % shopCams.Length;
        shopCams[camIdx].enabled = true;

    }

    void SwitchCam(int newCam)
    {
        for (int i = 0; i < shopCams.Length; i++)
        {
            if (i == newCam)
            {
                shopCams[i].enabled = true;
            }
            else
            {
                shopCams[i].enabled = false;
            }
        }
        camIdx = newCam;
    }
}
