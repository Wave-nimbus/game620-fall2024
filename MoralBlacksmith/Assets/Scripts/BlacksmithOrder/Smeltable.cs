using Unity.VisualScripting;
using UnityEngine;

public class Smeltable : MonoBehaviour
{
    [SerializeField]
    SmeltStatistics mySmeltStats;
    [SerializeField]
    SpriteRenderer mySpRend;
    
    public SmeltStatistics GetSmeltStats() { return mySmeltStats; }

    public void SetCurrSmeltAmount(float amount)
    {
        mySmeltStats.currSmeltAmount = amount;
        
    }

    public void SetSmeltStats(SmeltStatistics newStats, Sprite sprite)
    {
        mySmeltStats = newStats;
        mySpRend.sprite = sprite;
        mySpRend.size = new Vector2(75, 75);
    }

    public Sprite GetSprite()
    {
        return mySpRend.sprite;
    }
}
