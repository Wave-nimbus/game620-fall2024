using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager_MainMenu : MonoBehaviour
{
    [SerializeField]
    GameObject menuPanel, instructionPanel, introPanel;
    [SerializeField]
    Button menuStart, menuInstruction, menuQuit;
    [SerializeField]
    Button instructionBack, instructionStart;
    [SerializeField]
    Button introStart;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menuPanel.SetActive(true);
        instructionPanel.SetActive(false);
        introPanel.SetActive(false);
    }

    public void MenuStart()
    {
        menuPanel.SetActive(false);
        instructionPanel.SetActive(false);
        introPanel.SetActive(true);
    }

    public void MenuInstruction()
    { 
        menuPanel.SetActive(false);
        instructionPanel.SetActive(true);
        introPanel.SetActive(false);
    }

    public void MenuQuit()
    {
        Application.Quit();
    }

    public void InstructionBack()
    {
        menuPanel.SetActive(true);
        instructionPanel.SetActive(false);
        introPanel.SetActive(false);
    }

    public void InstructionStart()
    {
        menuPanel.SetActive(false);
        instructionPanel.SetActive(false);
        introPanel.SetActive(true);
    }

    public void IntroStart()
    {
        SceneManager.LoadScene(1);
    }

}
