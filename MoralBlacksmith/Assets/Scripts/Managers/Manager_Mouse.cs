using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Manager_Mouse : MonoBehaviour
{
    [SerializeField]
    GraphicRaycaster[] gRays;

    GraphicRaycaster currGRay;
    PointerEventData ptrEventData;
    EventSystem evSys;
    List<RaycastResult> results;

    int currCamIndex;

    public event Action MaterialAreaClick;
    public event Action SmeltingAreaClick;
    public event Action SmeltingAreaHold;
    public event Action SmeltingAreaRelease;
    public event Action ForgeAreaLeftClick;
    public event Action ForgeAreaRightClick;
    public event Action ForgeAreaHold;
    public event Action ForgeAreaRelease;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currGRay = gRays[0];
        evSys = EventSystem.current;
        ptrEventData = new PointerEventData(evSys);
        results = new List<RaycastResult>();
        currCamIndex = 0;
        FindFirstObjectByType<Manager_Cam>().CamUpdate += UpdateCameraIndex;
    }

    // Update is called once per frame
    void Update()
    {
        //ClickHandler();
        if (Input.GetMouseButtonDown(0))
        {
            LeftClickHandler(currCamIndex);
        }
        else if(Input.GetMouseButtonDown(1))
        {
            RightClickHandler(currCamIndex);
        }
        else if(Input.GetMouseButtonUp(0))
        {
            ReleaseHandler(currCamIndex);
        }
        
        if(Input.GetMouseButton(0))
        {
            HoldHandler(currCamIndex);
        }
    }

    void RightClickHandler(int camIndex)
    {
        switch (camIndex)
        {
            case 0:
                break;

            case 1:
                break;

            case 2:
                ForgeAreaRightClick?.Invoke();
                break;

            default:
                break;
        }
    }

    void LeftClickHandler(int camIndex)
    {
        switch(camIndex)
        {
            case 0:
                MaterialAreaClick?.Invoke();
                break;

            case 1:
                SmeltingAreaClick?.Invoke();
                break; 

            case 2:
                ForgeAreaLeftClick?.Invoke();
                break;

            default:
                break;
        }
    }

    void HoldHandler(int camIndex)
    {
        switch (camIndex)
        {
            case 0:
                break;

            case 1:
                SmeltingAreaHold?.Invoke();
                break;

            case 2:
                ForgeAreaHold?.Invoke();
                break;
            
            default:
                break;
        }
    }

    void ReleaseHandler(int camIndex)
    {
        switch (camIndex)
        {
            case 0:
                break;

            case 1:
                SmeltingAreaRelease?.Invoke();
                break;

            case 2:
                ForgeAreaRelease?.Invoke();
                break;

            default:
                break;
        }
    }

    void UpdateCameraIndex(int newCamIndex)
    {
        currCamIndex = newCamIndex;
    }


    void ClickHandler()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click!");
            //Graphical Raycasts used for UI elements.
            evSys = EventSystem.current;
            ptrEventData = new PointerEventData(evSys);
            ptrEventData.position = Input.mousePosition;
            results = new List<RaycastResult>();
            currGRay.Raycast(ptrEventData, results);
            Debug.Log("Pointer Vent Data: " + ptrEventData);
            Debug.Log("Raycast Results: " + results.Count);

            foreach (RaycastResult rr in results)
            {
                Debug.Log("Clicked on " + rr);
                bool clickedOnThing = false;
                switch (rr.gameObject.tag)
                {
                    case "MaterialArea":
                        MaterialAreaClick?.Invoke();
                        clickedOnThing = true;
                        break;

                    case "SmeltingArea":
                        SmeltingAreaClick?.Invoke();
                        clickedOnThing = true;
                        break;

                    case "ForgeArea":
                        ForgeAreaLeftClick?.Invoke();
                        clickedOnThing = true;
                        break;

                    default:
                        Debug.Log("Clicked on nothing");
                        clickedOnThing = false;
                        break;
                }
                if (clickedOnThing)
                {
                    break;
                }
            }
        }
    }

}
