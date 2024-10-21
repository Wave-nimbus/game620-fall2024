using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class ForgeRing : MonoBehaviour
{
    //Left MB click & hold = Activation.
    //Right MB click = Hammer attempt. Exit after.
    //Left MB release = Exit.
    [SerializeField]
    Image targetRing;
    [SerializeField]
    Image inputRing;

    [SerializeField]
    float tarRingRadius, inpRingRadius, maxRadius, minRadius;

    [SerializeField]
    float scaleSpeed;

    bool isClicked;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tarRingRadius = 125;
        maxRadius = 200;
        minRadius = 50;
        scaleSpeed = 300;
        inpRingRadius = Random.Range(minRadius, maxRadius);
        inputRing.rectTransform.sizeDelta = new Vector2(inpRingRadius, inpRingRadius);
        targetRing.rectTransform.sizeDelta = new Vector2(tarRingRadius, tarRingRadius);

        inputRing.gameObject.SetActive(false);
        isClicked = false;
    }

    // Update is called once per frame
    void Update()
    {
        //MouseHandler();
        if (isClicked)
        {
            GrowShrink();
        }
        

    }

    void MouseHandler()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GraphicRaycaster gRay = GetComponent<GraphicRaycaster>();
            PointerEventData ptrEventData;
            EventSystem evSys = EventSystem.current;
            
            ptrEventData = new PointerEventData(evSys);
            ptrEventData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            gRay.Raycast(ptrEventData, results);
            
            foreach(RaycastResult rr in results)
            {
                if(rr.gameObject.transform == targetRing.transform)
                {
                    Debug.Log("Clicked on the Target Ring.");
                    inputRing.gameObject.SetActive(true);
                    isClicked = true;
                }
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            inputRing.gameObject.SetActive(false);
            isClicked = false;
        }

        if(isClicked && Input.GetMouseButton(0))
        {
            if(Input.GetMouseButtonDown(1))
            {
                scaleSpeed = 0;
                Debug.Log("Difference in Ring Size: " + Mathf.Abs(tarRingRadius - inpRingRadius));
                Destroy(gameObject, 1);
            }
        }

    }

    public void WasLeftClicked()
    {
        inputRing.gameObject.SetActive(true);
        isClicked = true;
    }

    public float WasRightClicked()
    {
        scaleSpeed = 0;
        float retVal = Mathf.Abs(tarRingRadius - inpRingRadius);
        Debug.Log("Difference in Ring Size: " + retVal);
        //Destroy(gameObject, 1);
        return retVal;
    }

    public void WasLeftReleased()
    {
        inputRing.gameObject.SetActive(false);
        isClicked = false;
    }


    void GrowShrink()
    {
        inpRingRadius += scaleSpeed * Time.deltaTime;
        if (inpRingRadius > maxRadius)
        {
            inpRingRadius = maxRadius;
            scaleSpeed = -scaleSpeed;
        }
        else if (inpRingRadius < minRadius)
        {
            inpRingRadius = minRadius;
            scaleSpeed = -scaleSpeed;
        }
        inputRing.rectTransform.sizeDelta = new Vector2(inpRingRadius, inpRingRadius);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            scaleSpeed = 0;
            Debug.Log("Difference in Ring Size: " + Mathf.Abs(tarRingRadius - inpRingRadius));
        }
    }

}
