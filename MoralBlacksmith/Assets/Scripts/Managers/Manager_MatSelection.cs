using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Manager_MatSelection : MonoBehaviour
{
    [SerializeField]
    Image objPanel, handlePanel, metalPanel; //Material Selection
    [SerializeField]
    Image colObjPanel, colHandlePanel, colMetalPanel; //Collector at bottom
    [SerializeField]
    Image colObjImage, colHandleImage1, colHandleImage2, colMetalImage1, colMetalImage2, colMetalImage3; //Collected images.
    [SerializeField]
    Image orderPanel; //Order text & panel.
    [SerializeField]
    TMP_Text orderText;
    [SerializeField]
    Image btnReset, btnContinue;

    [SerializeField]
    OrderableObj currObj;


    ForgeObj currObjType;
    MatSelState currState;


    Color btnContinueEnabled = new Color(0, 1, 0, 1);
    Color btnContinueDisabled = new Color(0, 1, 0, 0.5f);
    Color colImageDisabled = new Color(1, 1, 1, 0.5f);
    Color colImageEnabled = Color.white;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        orderText.text = "";
        ResetCreator();
        FindFirstObjectByType<Manager_Mouse>().MaterialAreaClick += MaterialAreaHandler;
    }


    // Update is called once per frame
    void Update()
    {
        ApplyState();
    }

    void MaterialAreaHandler()
    {
        GraphicRaycaster gRay = GetComponent<GraphicRaycaster>();
        PointerEventData ptrEventData;
        EventSystem evSys = EventSystem.current;

        ptrEventData = new PointerEventData(evSys);
        ptrEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        gRay.Raycast(ptrEventData, results);

        foreach (RaycastResult rr in results)
        {
            //Debug.Log(rr);
            SelectionSpot selection;
            if (rr.gameObject.TryGetComponent<SelectionSpot>(out selection))
            {
                HandleSelectionState(selection);
                break;
            }
            else if (rr.gameObject.name == btnReset.gameObject.name)
            {
                ResetCreator();
            }
            else if (rr.gameObject.name == btnContinue.gameObject.name)
            {
                AdvanceCreator();
            }
        }
    }

    void ApplyState()
    {
        switch (currState) 
        {
            case MatSelState.chooseObj: //Choosing the object's form. Only ObjPanel should be turned on.
                objPanel.gameObject.SetActive(true);
                handlePanel.gameObject.SetActive(false);
                metalPanel.gameObject.SetActive(false);

                colObjPanel.gameObject.SetActive(true);
                colHandlePanel.gameObject.SetActive(false);
                colMetalPanel.gameObject.SetActive(false);

                orderPanel.gameObject.SetActive(true);
                break;

            case MatSelState.chooseHandle1:
                objPanel.gameObject.SetActive(true);
                handlePanel.gameObject.SetActive(true);
                metalPanel.gameObject.SetActive(false);

                colObjPanel.gameObject.SetActive(true);
                colHandlePanel.gameObject.SetActive(true);
                colMetalPanel.gameObject.SetActive(false);

                orderPanel.gameObject.SetActive(true);
                break;

            case MatSelState.chooseHandle2:
                //Panels exist in same state as chooseHandle1. No changes.
                break;

            case MatSelState.chooseMetal1: //Choosing object's Metal. All panels should be on.
                objPanel.gameObject.SetActive(true);
                handlePanel.gameObject.SetActive(true);
                metalPanel.gameObject.SetActive(true);

                colObjPanel.gameObject.SetActive(true);
                colHandlePanel.gameObject.SetActive(true);
                colMetalPanel.gameObject.SetActive(true);

                orderPanel.gameObject.SetActive(true);
                break;

            case MatSelState.chooseMetal2:
                //Panels exist in same state ass chooseMetal1. No changes.
                break;

            case MatSelState.chooseMetal3:
                //Panels exist in same state ass chooseMetal1. No changes.
                break;

            case MatSelState.chooseComplete: //Turn on the continue button.
                btnContinue.color = btnContinueEnabled;
                btnContinue.raycastTarget = true;
                break;
        }

    }


    void HandleSelectionState(SelectionSpot sel) //Selection Spot is the box that can be clicked on.
    {
        switch (currState)
        {
            case MatSelState.chooseObj:
                if (sel.GetObjectType() != ForgeObj.none) //Make sure it was an object thing.
                {
                    currObj.UpdateStats(sel.GetObjectType());
                    currObjType = sel.GetObjectType();
                    colObjImage.sprite = sel.GetMyImage();
                    PrepMetalImages(sel.GetObjectType());
                    currState = MatSelState.chooseHandle1;
                }
                break;

            case MatSelState.chooseHandle1:
                if(sel.GetMaterialHandle() != Mat_Handle.none) //Make sure it's a handle material.
                {
                    currObj.UpdateStats(sel.GetMaterialHandle(), 0);
                    currState = MatSelState.chooseHandle2;
                    colHandleImage1.sprite = sel.GetMyImage();
                }
                break;

            case MatSelState.chooseHandle2:
                if (sel.GetMaterialHandle() != Mat_Handle.none) //Make sure it's a handle material.
                {
                    currObj.UpdateStats(sel.GetMaterialHandle(), 1);
                    currState = MatSelState.chooseMetal1;
                    colHandleImage2.sprite = sel.GetMyImage();
                }
                break;

            case MatSelState.chooseMetal1:
                if(sel.GetMaterialMetal() != Mat_Metal.none) //Make sure it's a metal material.
                {
                    currObj.UpdateStats(sel.GetMaterialMetal(), 0);
                    FillSmeltStats(sel.GetMaterialMetal(), 0);
                    colMetalImage1.sprite = sel.GetMyImage();
                    if(currObjType == ForgeObj.axe)
                    {
                        currState = MatSelState.chooseComplete;
                    }
                    else
                    {
                        currState = MatSelState.chooseMetal2;
                    }
                }
                break;

            case MatSelState.chooseMetal2:
                if (sel.GetMaterialMetal() != Mat_Metal.none) //Make sure it's a metal material.
                {
                    currObj.UpdateStats(sel.GetMaterialMetal(), 1);
                    FillSmeltStats(sel.GetMaterialMetal(), 1);
                    colMetalImage2.sprite = sel.GetMyImage();
                    if (currObjType == ForgeObj.scythe)
                    {
                        currState = MatSelState.chooseComplete;
                    }
                    else
                    {
                        currState = MatSelState.chooseMetal3;
                    }
                }
                break;

            case MatSelState.chooseMetal3:
                if (sel.GetMaterialMetal() != Mat_Metal.none) //Make sure it's a metal material.
                {
                    currObj.UpdateStats(sel.GetMaterialMetal(), 2);
                    FillSmeltStats(sel.GetMaterialMetal(), 2);
                    colMetalImage3.sprite = sel.GetMyImage();
                    currState = MatSelState.chooseComplete;
                }
                break;

            default: 
                break;
        }
    }

    
    void PrepMetalImages(ForgeObj obj)
    {
        switch(obj)
        {
            case ForgeObj.sword:
                colMetalImage1.color = colImageEnabled;
                colMetalImage2.color = colImageEnabled;
                colMetalImage3.color = colImageEnabled;
                break;

            case ForgeObj.axe:
                colMetalImage1.color = colImageEnabled;
                colMetalImage2.color = colImageDisabled;
                colMetalImage3.color = colImageDisabled;
                break;

            case ForgeObj.scythe:
                colMetalImage1.color = colImageEnabled;
                colMetalImage2.color = colImageEnabled;
                colMetalImage3.color = colImageDisabled;
                break;

            default:
                colMetalImage1.color = colImageEnabled;
                colMetalImage2.color = colImageEnabled;
                colMetalImage3.color = colImageEnabled;
                break;

        }
    }
    
    //Official Smeltable Metal Stats
    void FillSmeltStats(Mat_Metal metalType, int index)
    {
        SmeltStatistics retStats = new SmeltStatistics();
        switch(metalType)
        {
            case Mat_Metal.bronze:
                retStats.maxSmeltAmount = 100;
                retStats.smeltRate = 10;
                retStats.desiredSmeltAmount = 25;
                retStats.currSmeltAmount = 0;
                currObj.UpdateStats(retStats, index);
                break;

            case Mat_Metal.springSteel:
                retStats.maxSmeltAmount = 150;
                retStats.smeltRate = 15;
                retStats.desiredSmeltAmount = 75;
                retStats.currSmeltAmount = 0;
                currObj.UpdateStats(retStats, index);
                break;

            case Mat_Metal.mythril:
                retStats.maxSmeltAmount = 300;
                retStats.smeltRate = 20;
                retStats.desiredSmeltAmount = 225;
                retStats.currSmeltAmount = 0;
                currObj.UpdateStats(retStats, index);
                break;
        }
    }


    void ResetCreator()
    {
        objPanel.gameObject.SetActive(true);
        handlePanel.gameObject.SetActive(false);
        metalPanel.gameObject.SetActive(false);

        colObjPanel.gameObject.SetActive(true);
        colHandlePanel.gameObject.SetActive(false);
        colMetalPanel.gameObject.SetActive(false);

        colObjImage.sprite = null;
        colHandleImage1.sprite = null;
        colHandleImage2.sprite = null;
        colMetalImage1.sprite = null;
        colMetalImage2.sprite = null;
        colMetalImage3.sprite = null;

        colMetalImage1.color = colImageEnabled;
        colMetalImage2.color = colImageEnabled;
        colMetalImage3.color = colImageEnabled;

        orderPanel.gameObject.SetActive(true);
        //orderText.text = "";

        btnContinue.color = btnContinueDisabled;
        btnContinue.raycastTarget = false;
    
        currState = MatSelState.chooseObj;
        currObjType = ForgeObj.none;
        currObj.ClearStats();
    }

    void AdvanceCreator()
    {
        if(currState == MatSelState.chooseComplete)
        {
            Manager_Orders.ManOrd.AdvanceMatSel(currObj.GetObjStats());
            Invoke("ResetCreator", 3);
        }

    }

    public void PrepareMatSel(string ordText)
    {
        ResetCreator();
        orderText.text = ordText;
        //Debug.Log(ordText);
        if(ordText == null)
        {
            ordText = "ERROR";
        }
    }
    
    
    
    private enum MatSelState
    {
        chooseObj, 
        chooseHandle1, chooseHandle2,
        chooseMetal1, chooseMetal2, chooseMetal3, 
        chooseComplete
    };

}
