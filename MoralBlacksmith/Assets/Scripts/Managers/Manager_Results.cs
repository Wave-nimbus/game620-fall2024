using TMPro;
using UnityEngine;

public class Manager_Results : MonoBehaviour
{
    [SerializeField]
    TMP_Text[] resultList;

    int resCount;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        resCount = 0;
    }

    public void UpdateResultText(string resText)
    {
        resultList[resCount].text = resText;
        resCount++;
    }

}
