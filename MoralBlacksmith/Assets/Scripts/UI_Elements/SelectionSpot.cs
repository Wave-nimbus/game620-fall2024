using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionSpot : MonoBehaviour
{
    [SerializeField]
    protected ForgeObj objectType;
    [SerializeField]
    protected Mat_Handle handleMaterial;
    [SerializeField]
    protected Mat_Metal metalMaterial;
    [SerializeField]
    protected Image myImage;
    [SerializeField]
    protected int orderIndex;



    public Mat_Handle GetMaterialHandle() { return handleMaterial; }
    public Mat_Metal GetMaterialMetal() { return metalMaterial; }
    public ForgeObj GetObjectType() {  return objectType; }
    public Sprite GetMyImage() { return myImage.sprite; }
    public void SetMyImage(Sprite newSprite) {  myImage.sprite = newSprite; }

    public int GetOrderIndex() { return orderIndex; }   


}

