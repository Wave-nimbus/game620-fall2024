using UnityEngine;
using UnityEngine.Rendering;

public class Scythe_Bronze : FullOrder
{
    protected override void Start()
    {
        requestedObj.wantedObjType = ForgeObj.scythe;
        requestedObj.wantedHandle1 = Mat_Handle.mapleWood;
        requestedObj.wantedHandle2 = Mat_Handle.oakWood;
        requestedObj.wantedMetal1 = Mat_Metal.bronze;
        requestedObj.wantedMetal2 = Mat_Metal.bronze;
        requestedObj.wantedMetal3 = Mat_Metal.none;
        ClearCreatedObj();
        CreateOrderText();
        CreateSuccessText();
        CreateFailText();
    }

    protected override void CreateOrderText()
    {

        orderText =
            "By order of your local feudal lord, the increase of crops has caused the demand for <u>" + requestedObj.wantedObjType +
            "</u> to rise dramatically to meet demand. You are tasked with assisting the creation of them. No need to spend " + 
            "too many resources. A mixed <u>" + requestedObj.wantedHandle1 + "</u> and <u>" + requestedObj.wantedHandle2  + 
            "</u> handle should suffice. The tines themselves can be made out of <u>" + requestedObj.wantedMetal1 + "</u> the " +
            "entire way in order to keep costs down.";

    }
    protected override void CreateSuccessText()
    {
        successText =
            "The increased produciton thanks to your pitchfork will help the farmers prepare their fields. " +
            "The feudal lord is pleased with the increased production and the farmers are happy with their tools.";
    }

    protected override void CreateFailText()
    {
        failText =
            "Early in the planting season, the pitchforks broke. Without the means to acquire new tools, the farmers aren't able to meet the lord's quota. " +
            "You are given a warning as the lord is forced to use his own supplies to prevent widespread famine.";
    }

}
