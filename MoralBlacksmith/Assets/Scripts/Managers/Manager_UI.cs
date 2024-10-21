using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class Manager_UI : MonoBehaviour
{
    Vector2 mousePos = Vector2.zero;

    Collider2D clickedObjCol;
    GameObject clickedObj;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        clickedObj = null;
        clickedObjCol = null;
    }

    // Update is called once per frame
    void Update()
    {
        //Did the left mouse button get clicked?
        if(Input.GetMouseButtonDown(0))
        {
            //Find if it clicked on anything.
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickedObjCol = Physics2D.OverlapPoint(mousePos);
            if (clickedObjCol != null && clickedObjCol.gameObject.CompareTag("Draggable"))
            {
                clickedObj = clickedObjCol.gameObject;
            }
        }
        
        if(Input.GetMouseButtonUp(0))
        {
            clickedObj = null;
            clickedObjCol = null;
        }

        if(clickedObj != null)
        {
            clickedObj.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
