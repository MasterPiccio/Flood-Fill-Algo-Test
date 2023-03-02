using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public GameObject pausePanel;
    public GameObject gamePanel;

    public GameObject WelcomeText;
    public GameObject STDText;
    public GameObject nextButton;

    public Text lenghtText, heightText, radiusText, DesiredLevelText;

    public TilesController tilesController;
    public CameraController cameraController;
    // Start is called before the first frame update
    void Start()
    {
        PauseGame();
        ShowWelcomeText();

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void GetInputs()
    {
        //convert input text lenght to int and set the grid lenght dimension
        if(string.IsNullOrEmpty(lenghtText.text))
        {
            tilesController.lenght = 10;
        }
        else
        {
            tilesController.lenght = int.Parse(lenghtText.text);
        }
        //convert input text height to int and set the grid height dimension
        if(string.IsNullOrEmpty(heightText.text))
        {
            tilesController.height =10;
        }
        else
        {
            tilesController.height = int.Parse(heightText.text);
        }
        //the same for radius
        if(string.IsNullOrEmpty(radiusText.text))
        {
            tilesController.radius = 2;
        }
        else
        {
            tilesController.radius = int.Parse(radiusText.text);
        }
        //and for desiredlevel
        if(string.IsNullOrEmpty(DesiredLevelText.text))
        {
           tilesController.desiredlevel = tilesController.Maxlevel/2; 
        }
        else
        {
            tilesController.desiredlevel = int.Parse(DesiredLevelText.text);
        }
    }


    public void PauseGame()
    {

        Time.timeScale = 0;
        pausePanel.SetActive(true);
        gamePanel.SetActive(false);

    }

    public void UnpauseGame()
    {
        Time.timeScale =1;
        pausePanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void ShowWelcomeText()
    {
        WelcomeText.SetActive(true);
        STDText.SetActive(false);
        nextButton.SetActive(true);
    }

    public void ShowValueText()
    {
        WelcomeText.SetActive(false);
        STDText.SetActive(true);
        nextButton.SetActive(false);
    }

}
