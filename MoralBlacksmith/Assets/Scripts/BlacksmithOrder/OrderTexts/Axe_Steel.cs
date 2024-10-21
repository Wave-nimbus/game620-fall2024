using UnityEngine;
using UnityEngine.Rendering;

public class Axe_Steel : FullOrder
{
    protected override void Start()
    {
        requestedObj.wantedObjType = ForgeObj.axe;
        requestedObj.wantedHandle1 = Mat_Handle.mapleWood;
        requestedObj.wantedHandle2 = Mat_Handle.iron;
        requestedObj.wantedMetal1 = Mat_Metal.springSteel;
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
            "Howdy. My most recent woodchoppin' axe just broke, so I'm lookin' to get a new <u>" + requestedObj.wantedObjType +
            "</u>. It broke near the axe's head, so I think it should be reinforced with <u>" + requestedObj.wantedHandle2 +
            "</u> while the handle is a solid <u>" + requestedObj.wantedHandle1 + "</u>. I don't need anything fancy for the" +
            "bit itself. Just some kind of <u>" + requestedObj.wantedMetal1 + "</u> should do for me";
    }

    protected override void CreateSuccessText()
    {
        successText =
            "The lumberjack was able to chop plenty of wood for the season. He made a little profit and had a comfortable winter.";
    }

    protected override void CreateFailText()
    {
        failText =
            "The lumberjack's new axe failed when he needed it most. His family will not have the wood for the winter. It'll be hard. And not all of them will survive.";
    }

}
