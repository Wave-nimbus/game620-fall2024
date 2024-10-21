using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Manager_Forge : MonoBehaviour
{
    [SerializeField]
    GameObject longswordPrefab, axePrefab, pitchforkPrefab;
    [SerializeField]
    TMP_Text evalText;
    [SerializeField]
    Image btnContinue;

    GameObject currOrder;
    GameObject currForgeImage;
    ForgeRing currRing;
    float currInaccuracy;
    int ringCountTotal;
    int ringCountRemains;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currOrder = null;
        currInaccuracy = 0;
        ringCountTotal = 0;
        ringCountRemains = ringCountTotal;
        currRing = null;
        evalText.text = "";

        Manager_Mouse mouseMan = FindFirstObjectByType<Manager_Mouse>();
        mouseMan.ForgeAreaLeftClick += ForgeLeftClickHandler;
        mouseMan.ForgeAreaRightClick += ForgeRigthClickHandler;
        mouseMan.ForgeAreaHold += ForgeLeftHoldHandler;
        mouseMan.ForgeAreaRelease += ForgeLeftReleaseHandler;
    }

    void ForgeLeftClickHandler()
    {
        //Handles UI Objects Only.
        GraphicRaycaster gRay = GetComponent<GraphicRaycaster>();
        PointerEventData ptrEventData;
        EventSystem evSys = EventSystem.current;

        ptrEventData = new PointerEventData(evSys);
        ptrEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        gRay.Raycast(ptrEventData, results);

        foreach(RaycastResult rr in results)
        {
            Debug.Log(rr);
            if(rr.gameObject.TryGetComponent<ForgeRing>(out currRing))
            {
                currRing.WasLeftClicked();
                return;
            }
            else if (rr.gameObject.name == btnContinue.gameObject.name)
            {
                AdvanceForge();
            }
        }
    }

    void ForgeLeftHoldHandler()
    {
        //Not used.
    }

    void ForgeLeftReleaseHandler()
    {
        if (currRing != null)
        {
            currRing.WasLeftReleased();
            currRing = null;
        }
    }

    void ForgeRigthClickHandler()
    {
        if (currRing != null)
        {
            float inaccuracy = currRing.WasRightClicked();
            ringCountRemains--;
            currInaccuracy += inaccuracy;
            Destroy(currRing.gameObject, 1);

            Debug.Log(currInaccuracy / ringCountTotal);

            if(ringCountRemains == 0)
            {
            }
        }
    }

    void AdvanceForge()
    {
        Manager_Orders.ManOrd.AdvanceForge(currOrder.GetComponent<FullOrder>().GetCreatedObj());
        Destroy(currForgeImage, 1);
    }

    public float ReportForgeResults()
    {
        return currInaccuracy / ringCountTotal;
    }

    public void DisplayResults(float eval)
    {
        
        evalText.text = "Last Completed Order Greatness: " + (eval * 100) % 100 + "%";
    }

    public void MakeForgeObject(ForgeObj type)
    {
        currForgeImage = null;
        switch(type)
        {
            case ForgeObj.scythe:
                currForgeImage = Instantiate(pitchforkPrefab, transform);
                ringCountTotal = 2;
                ringCountRemains = ringCountTotal;
                break;

            case ForgeObj.axe:
                currForgeImage = Instantiate(axePrefab, transform);
                ringCountTotal = 1;
                ringCountRemains = ringCountTotal;
                break;

            case ForgeObj.sword:
                currForgeImage = Instantiate(longswordPrefab, transform);
                ringCountTotal = 3;
                ringCountRemains = ringCountTotal;
                break;

            default:
                break;
        }
    }

    public void PrepareForge(FullOrder newOrder)
    {
        currOrder = newOrder.gameObject;
        MakeForgeObject(newOrder.GetCreatedObj().objectType);
    }



}
