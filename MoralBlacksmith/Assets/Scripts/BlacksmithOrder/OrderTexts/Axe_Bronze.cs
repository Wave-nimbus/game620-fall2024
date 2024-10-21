using UnityEngine;
using UnityEngine.Rendering;

public class Axe_Bronze : FullOrder
{
    protected override void Start()
    {
        requestedObj.wantedObjType = ForgeObj.axe;
        requestedObj.wantedHandle1 = Mat_Handle.oakWood;
        requestedObj.wantedHandle2 = Mat_Handle.oakWood;
        requestedObj.wantedMetal1 = Mat_Metal.bronze;
        requestedObj.wantedMetal2 = Mat_Metal.none;
        requestedObj.wantedMetal3 = Mat_Metal.none;
        ClearCreatedObj();
        CreateOrderText();
        CreateSuccessText();
        CreateFailText();
    }

    protected override void CreateOrderText()
    {
        orderText =
            "Hehehehe. Hey. Hey, buddy. I need a " + requestedObj.wantedObjType +
            "</u>. I don't need anything fancy. Just a pure <u>" + requestedObj.wantedHandle1 +
            "</u> handle with the head made of <u>" + requestedObj.wantedMetal1 + "</u>. Oh, and don't tell anyone about this, gotcha?";
    }

    protected override void CreateSuccessText()
    {
        successText =
            "The shady order turned out to be from a serial killer from the town over. And you just assisted his next murder." +
            "You'd better hope that axe you made can't be traced back to you, or you're in very big trouble.";
    }

    protected override void CreateFailText()
    {
        failText =
            "You recieve news that a serial killer was caught recently. Apparently, he tried to kill a noblewoman with a shoddily made bronze axe." +
            "The description of the axe matches something you made. You'd better hope that axe can't be traced back to you, or you're in big trouble";
    }

}
