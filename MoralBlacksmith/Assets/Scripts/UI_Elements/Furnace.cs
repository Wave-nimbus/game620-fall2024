using UnityEngine;
using UnityEngine.UI;

public class Furnace : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    Image furnaceInterior;
    [SerializeField]
    ProgressBarFurnace progBar;
    [SerializeField]
    Smeltable thingToSmelt;

    protected Color emptyFurnace, smeltingFurnace;

    void Start()
    {
        emptyFurnace = Color.black;
        smeltingFurnace = Color.red;
        thingToSmelt = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InsertSmeltable(Smeltable ingot)
    {
        if(thingToSmelt == null)
        {
            thingToSmelt = ingot;
            SmeltStatistics tempStats = ingot.GetSmeltStats();
            progBar.StartBar(tempStats.currSmeltAmount, tempStats.maxSmeltAmount, tempStats.smeltRate);
            furnaceInterior.color = smeltingFurnace;
        }
        else
        {
            Debug.Log("Furnace Full. Cannot Insert.");
        }
    }

    public Smeltable RemoveSmeltable()
    {
        thingToSmelt.SetCurrSmeltAmount(progBar.PauseBar());
        Smeltable temp = thingToSmelt;
        thingToSmelt = null;
        furnaceInterior.color = emptyFurnace;
        progBar.ResetBar();
        return temp;
    }

    public bool IsFull()
    {
        return thingToSmelt != null;
    }

}
