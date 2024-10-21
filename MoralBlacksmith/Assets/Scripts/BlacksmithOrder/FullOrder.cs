using System.Net.Sockets;
using UnityEngine;

public class FullOrder : MonoBehaviour
{
    [SerializeField]
    protected OrderRequirements requestedObj;
    [SerializeField]
    protected ObjStatistics createdObj;
    [SerializeField]
    protected string orderText;

    protected string successText;
    protected string failText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        ClearRequestObj();
        ClearCreatedObj();
        CreateOrderText();
        CreateSuccessText();
        CreateFailText();
    }

    protected void ClearRequestObj()
    {
        requestedObj.wantedObjType = ForgeObj.none;
        requestedObj.wantedHandle1 = Mat_Handle.none;
        requestedObj.wantedHandle2 = Mat_Handle.none;
        requestedObj.wantedMetal1 = Mat_Metal.none;
        requestedObj.wantedMetal2 = Mat_Metal.none;
        requestedObj.wantedMetal3 = Mat_Metal.none;
    }

    protected void ClearCreatedObj()
    {
        createdObj.objectType = ForgeObj.none;
        createdObj.handleType1 = Mat_Handle.none;
        createdObj.handleType2 = Mat_Handle.none;
        createdObj.metalType1 = Mat_Metal.none;
        createdObj.metalType2 = Mat_Metal.none;
        createdObj.metalType2 = Mat_Metal.none;
    }

    protected virtual void CreateOrderText()
    {
        orderText =
    "Object Type: <u>" + requestedObj.wantedObjType + "</u>\n\n" +
    "Handle Material 1: <u>" + requestedObj.wantedHandle1 + "</u>\n" +
    "Handle Material 2: <u>" + requestedObj.wantedHandle2 + "</u>\n\n" +
    "Metal Material 1: <u>" + requestedObj.wantedMetal1 + "</u>\n" +
    "Metal Material 2: <u>" + requestedObj.wantedMetal2 + "</u>\n" +
    "Metal Material 3: <u>" + requestedObj.wantedMetal3 + "</u>\n";
    }

    protected virtual void CreateSuccessText()
    {
        successText = "The order was made successfully";
    }

    protected virtual void CreateFailText()
    {
        failText = "The order was made incorrectlly or incompetantly";
    }

    public void SetRequestedObj(OrderRequirements requirements)
    {
        requestedObj = requirements;
    }

    public OrderRequirements GetRequestedObj()
    {
        return requestedObj;
    }
    public void SetCreatedObj(ObjStatistics creation)
    {
        createdObj = creation;
    }

    public ObjStatistics GetCreatedObj()
    {
        return createdObj;
    }
    public string GetOrderText()
    {
        return orderText;
    }

    public string GetSuccessText()
    {
        return successText;
    }

    public string GetFailText() 
    { 
        return failText;
    }

    public SmeltStatistics GetCreatedSmeltStats(int index)
    {
        SmeltStatistics ret = new SmeltStatistics();
        ret.maxSmeltAmount = 0;
        ret.currSmeltAmount = 0;
        ret.desiredSmeltAmount = 0;
        ret.currSmeltAmount = 0;
        switch (index)
        {
            case 0:
                ret = createdObj.smeltStats1;
                break;
            case 1:
                ret = createdObj.smeltStats2;
                break;
            case 2:
                ret = createdObj.smeltStats3;
                break;
            default:

                break;
        }
        return ret;
    }

    public void SetCreatedSmeltStats(SmeltStatistics smSt, int index)
    {
        switch (index)
        {
            case 0:
                createdObj.smeltStats1 = smSt;
                break;
            case 1:
                createdObj.smeltStats2 = smSt;
                break;
            case 2:
                createdObj.smeltStats3 = smSt;
                break;
            default:
                break;
        }
    }

}

public enum ForgeObj //Potential objects
{
    none, sword, axe, scythe
};
//Sword takes 3 metal, Axe takes 1 metal, pitchfork takes 2 metal.

public enum Mat_Handle //Handle materials
{
    none, oakWood, mapleWood, iron
};

public enum Mat_Metal //Metal materials
{
    none, bronze, springSteel, mythril
};

[System.Serializable]
public struct SmeltStatistics //Smelting statistics for metals.
{
    public float maxSmeltAmount;
    public float smeltRate;
    public float desiredSmeltAmount;
    public float currSmeltAmount;
}

[System.Serializable]
public struct ObjStatistics //Statistics of the created object.
{
    public ForgeObj objectType;
    public Mat_Handle handleType1;
    public Mat_Handle handleType2;
    public Mat_Metal metalType1;
    public Mat_Metal metalType2;
    public Mat_Metal metalType3;
    public SmeltStatistics smeltStats1;
    public SmeltStatistics smeltStats2;
    public SmeltStatistics smeltStats3;
}

public struct OrderRequirements //Statistics of the requested object.
{
    public ForgeObj wantedObjType;
    public Mat_Handle wantedHandle1;
    public Mat_Handle wantedHandle2;
    public Mat_Metal wantedMetal1;
    public Mat_Metal wantedMetal2;
    public Mat_Metal wantedMetal3;
}


