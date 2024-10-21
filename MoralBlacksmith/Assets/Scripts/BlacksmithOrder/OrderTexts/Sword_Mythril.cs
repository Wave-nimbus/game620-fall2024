using UnityEngine;
using UnityEngine.Rendering;

public class Sword_Mythril : FullOrder
{
    protected override void Start()
    {
        requestedObj.wantedObjType = ForgeObj.sword;
        requestedObj.wantedHandle1 = Mat_Handle.oakWood;
        requestedObj.wantedHandle2 = Mat_Handle.oakWood;
        requestedObj.wantedMetal1 = Mat_Metal.mythril;
        requestedObj.wantedMetal2 = Mat_Metal.mythril;
        requestedObj.wantedMetal3 = Mat_Metal.mythril;
        ClearCreatedObj();
        CreateOrderText();
        CreateSuccessText();
        CreateFailText();
    }

    protected override void CreateOrderText()
    {
       orderText =
            "Hey there! I'm an up and coming adventurer who just came into a spot of money. I'd really want" +
            "a new <u>" + requestedObj.wantedObjType + "</u> with a whole solid <u>" + requestedObj.wantedHandle1 +
            "</u> handle. And with my bonus money, I'd like to get the blade completely made out of <u>" + requestedObj.wantedMetal1 +
            "</u>. Would you be able to do that for me? Thanks!";
    }

    protected override void CreateSuccessText()
    {
        successText =
            "The adventurer that you helped took their sword to defeat a handful of monsters terrorizing a local trade route. " +
            "In the future, their heroism will be celebrated in taverns all across the land. And it all started with your sword.";
    }

    protected override void CreateFailText()
    {
        failText =
            "The sword snapped soon after its first swing against a monster that the adventurer encountered on the road. He barely escaped with his life." +
            "And he swore of adventuring forever, leaving the world a little less safe as the years go on.";
    }

}
