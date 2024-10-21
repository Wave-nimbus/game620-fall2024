using UnityEngine;
using UnityEngine.UI;

public class ProgressBarFurnace : MonoBehaviour
{
    [SerializeField]
    RectTransform barTransform;
    [SerializeField]
    Image barForeGround;
    [SerializeField]
    Image barBackGround;
    [SerializeField]
    Image trackerArrow;
    [SerializeField]
    float fillRate;

    protected float maxAmount, currAmount;
    protected Vector3 trackerPos;
    protected float trackerY;
    protected bool isFilling;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxAmount = 100;
        currAmount = 10;

        barForeGround.fillAmount = currAmount / maxAmount;
        fillRate = 5f;

        trackerY = trackerArrow.rectTransform.localPosition.y;
        trackerPos = new Vector3((barForeGround.fillAmount * barTransform.rect.width), trackerY, 0);
        trackerArrow.rectTransform.anchoredPosition = trackerPos;;
    }

    // Update is called once per frame
    void Update()
    {
        if(isFilling)
        {
            currAmount += fillRate * Time.deltaTime;
            barForeGround.fillAmount = currAmount / maxAmount;
            trackerPos.x = barForeGround.fillAmount * barTransform.rect.width;
            trackerArrow.rectTransform.anchoredPosition = trackerPos;
        }
    }

    public void StartBar(float startVal, float maxVal, float fillRateVal)
    {
        currAmount = startVal;
        maxAmount = maxVal;
        fillRate = fillRateVal;
        isFilling = true;
    }

    public float PauseBar()
    {
        isFilling = false;
        return currAmount;
    }

    public void ResetBar()
    {
        isFilling = false;
        barForeGround.fillAmount = 0;
        trackerPos.x = barForeGround.fillAmount * barTransform.rect.width;
        trackerArrow.rectTransform.anchoredPosition = trackerPos;
        
    }

}
