using NUnit.Framework;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class OrderableObj : MonoBehaviour
{
    [SerializeField]
    ObjStatistics currStats = new ObjStatistics(); //Viewable stats.

    public OrderableObj() //Empty constructor.
    {
        currStats.objectType = ForgeObj.none;
        currStats.handleType1 = Mat_Handle.none;
        currStats.handleType2 = Mat_Handle.none;
        currStats.metalType1 = Mat_Metal.none;
        currStats.metalType2 = Mat_Metal.none;
        currStats.metalType3 = Mat_Metal.none;
    }


    public void UpdateStats(ForgeObj objType)
    {
        currStats.objectType = objType;
    }
    public void UpdateStats(Mat_Handle hdlType, int index)
    {
        switch (index)
        {
            case 0:
                currStats.handleType1 = hdlType; 
                break;

            case 1:
                currStats.handleType2 = hdlType;
                break;

            default:
                break;
        }

    }
    public void UpdateStats(Mat_Metal metal, int index)
    {
        switch (index)
        {
            case 0:
                currStats.metalType1 = metal;
                break;

            case 1:
                currStats.metalType2 = metal;
                break;

            case 2:
                currStats.metalType3 = metal;
                break;

            default:
                break;
        }
    }
    public void UpdateStats(SmeltStatistics smeltStats, int index)
    {
        switch (index)
        {
            case 0:
                currStats.smeltStats1 = smeltStats;
                break;

            case 1:
                currStats.smeltStats2 = smeltStats;
                break;

            case 2:
                currStats.smeltStats3 = smeltStats;
                break;

            default:
                break;
        }
    }

    public void ClearStats()
    {
        currStats.objectType = ForgeObj.none;
        currStats.handleType1 = Mat_Handle.none;
        currStats.handleType2 = Mat_Handle.none;
        currStats.metalType1 = Mat_Metal.none;
        currStats.metalType2 = Mat_Metal.none;
        currStats.metalType3 = Mat_Metal.none;
    }

    public ObjStatistics GetObjStats()
    {
        return currStats;
    }
}
