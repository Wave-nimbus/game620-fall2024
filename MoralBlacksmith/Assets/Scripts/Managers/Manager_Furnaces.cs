using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEditor;

public class Manager_Furnaces : MonoBehaviour
{
    [SerializeField]
    Furnace[] furnaceArray;
    [SerializeField]
    Collider2D[] furnaceCollisions;
    [SerializeField]
    GameObject dragMetalPrefab;
    [SerializeField]
    FullOrder currOrder;
    [SerializeField]
    Camera furnaceCam;

    [SerializeField]
    Sprite sword, axe, scythe, oakWood, mapleWood, iron, bronze, springSteel, mythril;
    [SerializeField]
    Image colObjImage, colHandleImage1, colHandleImage2, colMetalImage1, colMetalImage2, colMetalImage3;

    [SerializeField]
    Image btnContinue;

    List<Collider2D> colResults = new List<Collider2D>();
    Collider2D furnCol;

    List<GameObject> draggableObjs = new List<GameObject>();

    GameObject draggedObj;
    Vector2 mouseWorldPos;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        colResults.Clear();
        furnCol = null;

        draggedObj = null;
        mouseWorldPos = Vector2.zero;

        Manager_Mouse mouseMan = FindFirstObjectByType<Manager_Mouse>();

        mouseMan.SmeltingAreaClick += SmeltingUIDownHandler;
        mouseMan.SmeltingAreaClick += SmeltingWorldDownHandler;
        mouseMan.SmeltingAreaHold += SmeltingWorldHoldHandler;
        mouseMan.SmeltingAreaRelease += SmeltingUIReleaseHandler;
        mouseMan.SmeltingAreaRelease += SmeltingWorldReleaseHandler;

        ClearCurrOrder();
        
    }

    // Update is called once per frame
    void Update()
    {
        //OldFurnaceHandler();
    }

    void OldFurnaceHandler()
    {
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < furnaceCollisions.Length; i++)
            {
                furnCol = furnaceCollisions[i];
                if (furnCol.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
                {
                    Smeltable removed = furnaceArray[i].RemoveSmeltable();
                    removed.transform.position = (Vector2)furnaceCollisions[i].transform.position + Vector2.down * 1.5f;
                    removed.gameObject.SetActive(true);

                }
            }
        }



        if (Input.GetMouseButtonUp(0)) //Check only when mouse is released.
        {
            for (int i = 0; i < furnaceCollisions.Length; i++)
            {
                if (furnaceArray[i].IsFull())
                {
                    continue;
                }
                furnCol = furnaceCollisions[i];
                furnCol.Overlap(colResults);
                if (colResults.Count > 0)
                {
                    foreach (Collider2D col in colResults)
                    {
                        if (col.gameObject.GetComponent<Smeltable>() != null)
                        {
                            furnaceArray[i].InsertSmeltable(col.gameObject.GetComponent<Smeltable>());
                            //Debug.Log("Inserted an ingot to furnace " + i);
                            col.gameObject.SetActive(false);
                            return;
                        }
                    }
                }
            }
        }
    }

    void SmeltingUIDownHandler()
    {
        //Handles UI Objects Only.
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
                IconToObect(selection);
                break;
            }
            else if (rr.gameObject.name == btnContinue.gameObject.name)
            {
                AdvanceFurnace();
            }
        }
    }

    void SmeltingUIReleaseHandler()
    {
        if(draggedObj == null) //Not draggining anything
        {
            return;
        }

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
            //Debug.Log(rr);
            SelectionSpot selection;
            if (rr.gameObject.TryGetComponent<SelectionSpot>(out selection))
            {
                ObjectToIcon(selection, draggedObj.gameObject.GetComponent<Smeltable>());
                break;
            }
        }
    }

    void SmeltingWorldDownHandler()
    {
        //Find if it clicked on anything.
        mouseWorldPos = furnaceCam.ScreenToWorldPoint(Input.mousePosition);
        Collider2D clickedObjCol = Physics2D.OverlapPoint(mouseWorldPos);
        //Clicked on a Draggable thing.
        if (clickedObjCol != null) 
        {
            if (draggableObjs.IndexOf(clickedObjCol.gameObject) != -1)
            {
                draggedObj = clickedObjCol.gameObject;
            }
            else //Clicked on a Furnace?
            {
                for (int i = 0; i < furnaceCollisions.Length; i++)
                {
                    furnCol = furnaceCollisions[i];
                    if (furnCol.transform == clickedObjCol.transform)
                    {
                        Smeltable removed = furnaceArray[i].RemoveSmeltable();
                        removed.transform.position = (Vector2)furnaceCollisions[i].transform.position + Vector2.down * 1.5f;
                        removed.gameObject.SetActive(true);

                    }
                }
            }
        }
    }

    void SmeltingWorldHoldHandler()
    {
        if(draggedObj != null)
        {
            draggedObj.transform.position = (Vector2) furnaceCam.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    void SmeltingWorldReleaseHandler()
    {
        for (int i = 0; i < furnaceCollisions.Length; i++) //For all furnaces
        {
            if (furnaceArray[i].IsFull())
            {
                continue;
            }
            furnCol = furnaceCollisions[i];
            furnCol.Overlap(colResults); //Find anything overlapping with it, put in colResults.
            if (colResults.Count > 0)
            {
                foreach (Collider2D col in colResults) //For any overlaps,
                {
                    if (col.gameObject.GetComponent<Smeltable>() != null) //If it was a Smeltable, stick it inside.
                    {
                        furnaceArray[i].InsertSmeltable(col.gameObject.GetComponent<Smeltable>());
                        //Debug.Log("Inserted an ingot to furnace " + i);
                        col.gameObject.SetActive(false);
                        return;
                    }
                }
            }
        }
        draggedObj = null;
    }


    void IconToObect(SelectionSpot sel)
    {
        if(sel.GetMaterialMetal() == Mat_Metal.none || sel.GetMyImage() == null)
        {
            return;
        }
        else
        {
            //Debug.Log("Generating draggable thing.");
            GameObject newobj = Instantiate(dragMetalPrefab, sel.transform.position, Quaternion.identity);
            newobj.transform.position += Vector3.up * 2f;
            newobj.transform.position -= new Vector3(0, 0, 90);
            newobj.GetComponent<Smeltable>().SetSmeltStats(currOrder.GetCreatedSmeltStats(sel.GetOrderIndex()), sel.GetMyImage());
            newobj.GetComponent<SpriteRenderer>().size = new Vector2(75, 75);
            draggableObjs.Add(newobj);
            sel.SetMyImage(null);
        }
    }

    void ObjectToIcon(SelectionSpot spot, Smeltable smelt)
    {
        if(spot.GetMaterialMetal() == Mat_Metal.none || spot.GetMyImage() != null)
        {
            return;
        }
        else
        {
            //Debug.Log("Returning draggable to Icon.");
            spot.SetMyImage(smelt.GetSprite());
            currOrder.SetCreatedSmeltStats(smelt.GetSmeltStats(), spot.GetOrderIndex());
            draggableObjs.Remove(smelt.gameObject);
            Destroy(smelt.gameObject);
        }
    }


    public void SetCurrOrder(FullOrder focusOrder)
    {
        currOrder = focusOrder;
        colObjImage.sprite = TypeToImage(currOrder.GetCreatedObj().objectType);
        colHandleImage1.sprite = TypeToImage(currOrder.GetCreatedObj().handleType1);
        colHandleImage2.sprite = TypeToImage(currOrder.GetCreatedObj().handleType2);
        colMetalImage1.sprite = TypeToImage(currOrder.GetCreatedObj().metalType1);
        colMetalImage2.sprite = TypeToImage(currOrder.GetCreatedObj().metalType2);
        colMetalImage3.sprite = TypeToImage(currOrder.GetCreatedObj().metalType3);
    }

    public void ClearCurrOrder()
    {
        currOrder = null;
        colObjImage.sprite = null;
        colHandleImage1.sprite = null;
        colHandleImage2.sprite = null;
        colMetalImage1.sprite = null;
        colMetalImage2.sprite = null;
        colMetalImage3.sprite = null;
    }

    Sprite TypeToImage(ForgeObj type)
    {
        Sprite retSprite = null;
        switch(type)
        {
            case ForgeObj.sword:
                return sword;

            case ForgeObj.axe:
                return axe;

            case ForgeObj.scythe:
                return scythe;
        }
        return retSprite;
    }

    Sprite TypeToImage(Mat_Handle type)
    {
        Sprite retSprite = null;
        switch (type)
        {
            case Mat_Handle.oakWood:
                return oakWood;

            case Mat_Handle.mapleWood:
                return mapleWood;

            case Mat_Handle.iron:
                return iron;
        }
        return retSprite;
    }

    Sprite TypeToImage(Mat_Metal type)
    {
        Sprite retSprite = null;
        switch (type)
        {
            case Mat_Metal.bronze:
                return bronze;

            case Mat_Metal.springSteel:
                return springSteel;

            case Mat_Metal.mythril:
                return mythril;
        }
        return retSprite;
    }

    void AdvanceFurnace() //When advancement button is pressed.
    {
        Manager_Orders.ManOrd.AdvanceFurnaces(currOrder.GetComponent<FullOrder>().GetCreatedObj());
    }


}
