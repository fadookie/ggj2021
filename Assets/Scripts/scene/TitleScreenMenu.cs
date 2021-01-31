using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenMenu : MonoBehaviour
{

    public void startGame(){
        SceneManager.LoadScene("Intro");
    }
    
    public void startShipGame(){
        SceneManager.LoadScene("ShipGame");
    }
    public void mainMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }
    public void quit()
    {
        Application.Quit();
    }
}
