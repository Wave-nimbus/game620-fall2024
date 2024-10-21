using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;
using System.Data;
using Unity.VisualScripting;

public class Manager_Orders : MonoBehaviour
{
    //Bunch of GameObjects with "FullOrder" objects on them.
    [SerializeField]
    List<GameObject> demoOrders;
    [SerializeField]
    GameObject matSelOrder;
    [SerializeField]
    List<GameObject> smeltOrders;
    [SerializeField]
    List<GameObject> forgeOrders;
    [SerializeField]
    List<GameObject> potentialOrders;
    

    Manager_MatSelection ManMat;
    Manager_Furnaces ManFrn;
    Manager_Forge ManFge;
    Manager_Results ManRes;


    public static Manager_Orders ManOrd;

    private void Awake()
    {
        if (Manager_Orders.ManOrd == null)
        {
            Manager_Orders.ManOrd = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        demoOrders = new List<GameObject>();
        matSelOrder = null;
        smeltOrders = new List<GameObject>();
        forgeOrders = new List<GameObject>();


        smeltOrders.Clear();
        forgeOrders.Clear();

        ManMat = FindFirstObjectByType<Manager_MatSelection>();
        ManFrn = FindFirstObjectByType<Manager_Furnaces>();
        ManFge = FindFirstObjectByType<Manager_Forge>();
        ManRes = FindFirstObjectByType<Manager_Results>();

        RandomizeOrders();
        //GenerateOrder(5);
        Invoke("StartOrders", 0.1f);
        
    }

    void MoveOrder(FullOrder order, SmithStage currStage, SmithStage nextStage)
    {
        if (order == null)
        {
            return;
        }
        
        switch(currStage)
        {
            case SmithStage.matSel:
                matSelOrder = null;
                break;

            case SmithStage.smelt:
                smeltOrders.Remove(order.gameObject);
                break;

            case SmithStage.forge:
                forgeOrders.Remove(order.gameObject);
                break;

            default:
                break;
        }

        switch (nextStage)
        {
            case SmithStage.matSel:
                matSelOrder = order.gameObject;
                break;

            case SmithStage.smelt:
                smeltOrders.Add(order.gameObject);
                break;

            case SmithStage.forge:
                forgeOrders.Add(order.gameObject);
                break;

            default:
                break;
        }
    }

    void GenerateOrder()
    {
        GameObject orderCopy = Instantiate(potentialOrders[Random.Range(0, potentialOrders.Count)]);
        demoOrders.Add(orderCopy);
    }

    void GenerateOrder(int numOrders)
    {
        for(int i = 0; i < numOrders; i++)
        {
            GenerateOrder();
        }
    }

    void StartOrders()
    {
        if(demoOrders.Count <= 0)
        {
            GenerateOrder();
        }

        matSelOrder = demoOrders[0];
        demoOrders.RemoveAt(0);
        ManMat.PrepareMatSel(matSelOrder.GetComponent<FullOrder>().GetOrderText());
    }

    public void AdvanceMatSel(ObjStatistics chosenObj)
    {
        matSelOrder.GetComponent<FullOrder>().SetCreatedObj(chosenObj);
        smeltOrders.Add(matSelOrder);
        ManFrn.SetCurrOrder(smeltOrders[0].GetComponent<FullOrder>());
        matSelOrder = null;
        if (demoOrders.Count > 0)
        {
            matSelOrder = demoOrders[0];
            demoOrders.RemoveAt(0);
            ManMat.PrepareMatSel(matSelOrder.GetComponent<FullOrder>().GetOrderText());
        }
        
    }

    public void AdvanceFurnaces(ObjStatistics smeltObj)
    {
        smeltOrders[0].GetComponent<FullOrder>().SetCreatedObj(smeltObj);
        forgeOrders.Add(smeltOrders[0]);
        smeltOrders.RemoveAt(0);
        ManFge.PrepareForge(forgeOrders[0].GetComponent<FullOrder>());
        if(smeltOrders.Count > 0)
        {
            ManFrn.SetCurrOrder(smeltOrders[0].GetComponent<FullOrder>());
        }
        else
        {
            ManFrn.ClearCurrOrder();
        }
    }

    public void AdvanceForge(ObjStatistics forgeObj)
    {
        forgeOrders[0].GetComponent<FullOrder>().SetCreatedObj(forgeObj);
        EvaluateObject(forgeOrders[0].GetComponent<FullOrder>());
        forgeOrders.RemoveAt(0);
        CheckOrders();
    }

    void RandomizeOrders()
    {
        demoOrders.Clear();
        foreach(GameObject obj in potentialOrders)
        {
            demoOrders.Add(Instantiate(obj));
        }
        int randTemp;
        GameObject switchTemp = null;
        for(int i = 0; i < demoOrders.Count; i++)
        {
            randTemp = Random.Range(0, demoOrders.Count - 1);
            switchTemp = demoOrders[i];
            demoOrders[i] = demoOrders[randTemp];
            demoOrders[randTemp] = switchTemp;
        }
    }

    void EvaluateObject(FullOrder completeOrder)
    {
        float correctness = 0f; //+1 for every correct thing & accuracy. Max of 12
        OrderRequirements reqs = completeOrder.GetRequestedObj();
        ObjStatistics currStats = completeOrder.GetCreatedObj();

        //Ensure Object matches.
        if (reqs.wantedObjType == currStats.objectType)
            correctness++;
        //Ensure Handles match. *Order Matters*.
        if(reqs.wantedHandle1 == currStats.handleType1)
            correctness++;
        if(reqs.wantedHandle2 == currStats.handleType2)
            correctness++;

        //Ensure Metals match. *Order Matters*.
        if(reqs.wantedMetal1 == currStats.metalType1)
            correctness++;
        if(reqs.wantedMetal2 == currStats.metalType2)
            correctness++;
        if(reqs.wantedMetal3 == currStats.metalType3)
            correctness++;
        //Evaluate Smelting ability.
        correctness += EvaluateSmeltStats(currStats.smeltStats1);
        correctness += EvaluateSmeltStats(currStats.smeltStats2);
        correctness += EvaluateSmeltStats(currStats.smeltStats3);

        //Evaluate Forging ability.
        correctness += EvalulateRingStats(ManFge.ReportForgeResults());
        ManFge.DisplayResults(correctness/12f);
        Debug.Log(correctness / 12f);

        if(correctness / 12f < 0.8f)
        {
            ManRes.UpdateResultText(completeOrder.GetFailText());
        }
        else
        {
            ManRes.UpdateResultText(completeOrder.GetSuccessText());
        }
    }

    float EvaluateSmeltStats(SmeltStatistics stats)
    {
        float diff = Mathf.Abs(stats.currSmeltAmount - stats.desiredSmeltAmount);
        if (diff < 5)
            return 1;
        else if (diff < 10)
            return 0.75f;
        else if (diff < 20)
            return 0.25f;
        else //if (diff > 20)
            return 0;

    }

    float EvalulateRingStats(float rings)
    {
        if (rings < 25)
            return 1;
        else if (rings < 50)
            return 0.75f;
        else if (rings < 100)
            return 0.5f;
        else //if rings > 100)
            return 0;
    }

    void CheckOrders()
    {
        if(demoOrders.Count <= 0 && matSelOrder == null && smeltOrders.Count <= 0 && forgeOrders.Count <= 0) 
        {
            FindFirstObjectByType<Manager_Cam>().ResultCam();
        }
    }
}

public enum SmithStage
    {
        matSel, smelt, forge
    };
