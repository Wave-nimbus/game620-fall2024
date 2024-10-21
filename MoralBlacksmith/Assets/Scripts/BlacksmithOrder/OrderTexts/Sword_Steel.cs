using UnityEngine;
using UnityEngine.Rendering;

public class Sword_Steel : FullOrder
{
    protected override void Start()
    {
        requestedObj.wantedObjType = ForgeObj.sword;
        requestedObj.wantedHandle1 = Mat_Handle.oakWood;
        requestedObj.wantedHandle2 = Mat_Handle.iron;
        requestedObj.wantedMetal1 = Mat_Metal.springSteel;
        requestedObj.wantedMetal2 = Mat_Metal.springSteel;
        requestedObj.wantedMetal3 = Mat_Metal.springSteel;
        ClearCreatedObj();
        CreateOrderText();
        CreateSuccessText();
        CreateFailText();
    }

    protected override void CreateOrderText()
    {
       orderText =
            "Good day, smith. I'm a guard for the East Gate, and I was thinking about commissioning you for a new <u>" +
            requestedObj.wantedObjType + "</u>. I'd like a comfortable <u>" + requestedObj.wantedHandle1 +
            "</u> hilt with a <u>" + requestedObj.wantedHandle2 + "</u> reinforced pommel and crossguard." 
            + "The blade should be sturdy and last me a long time, which makes <u>" + requestedObj.wantedMetal1 +
            "</u> an excellent choice. Looking forward to what you make on my behalf.";
    }

    protected override void CreateSuccessText()
    {
        successText =
            "The local guard was very pleased with his new sword, and it lasted him for years, occasionally being used to stop a bandit." +
            "He spread the word to his fellow guardsmen about where to get such well-crafted armaments, hopefully bringing you more business.";
    }

    protected override void CreateFailText()
    {
        failText =
            "The local guard's sword failed to live to his expectations, being subpar to even the garrison's standard issued swords. It was soon" +
            "abandoned, and by word of mouth, the garrison decided that they no longer trust your workmanship for any commissions, personal or official.";
    }

}
