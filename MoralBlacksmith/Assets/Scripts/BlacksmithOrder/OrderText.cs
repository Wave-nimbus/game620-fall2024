using UnityEngine;

public class OrderText : MonoBehaviour
{
    protected string orderText;
    [SerializeField]
    protected OrderRequirements requirements;

    protected virtual void Start()
    {
        CreateText();
    }

    protected virtual void CreateText()
    {
        orderText = 
            "Object Type: <u>" + requirements.wantedObjType  + "</u>\n\n" +
            "Handle Material 1: <u>" + requirements.wantedHandle1 + "</u>\n" +
            "Handle Material 2: <u>" + requirements.wantedHandle2 + "</u>\n\n" +
            "Metal Material 1: <u>" + requirements.wantedMetal1 + "</u>\n" +
            "Metal Material 2: <u>" + requirements.wantedMetal2 + "</u>\n" +
            "Metal Material 3: <u>" + requirements.wantedMetal3 + "</u>\n";
    }

    public string GetOrderText()
    {
        return orderText;
    }

    public OrderRequirements GetRequirements()
    {
        return requirements;
    }

    public void SetRequirements(OrderRequirements reqs)
    {
        requirements = reqs;
    }

    public void ClearRequirements()
    {
        requirements.wantedObjType = ForgeObj.none;
        requirements.wantedHandle1 = Mat_Handle.none;
        requirements.wantedHandle2 = Mat_Handle.none;
        requirements.wantedMetal1 = Mat_Metal.none;
        requirements.wantedMetal2 = Mat_Metal.none;
        requirements.wantedMetal3 = Mat_Metal.none;
    }

}

