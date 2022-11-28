using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public string firstLevel;
    public GameObject optionScreen;
    public GameObject characterSelectScreen;
    public void StartGame()
    {
        SceneManager.LoadScene((firstLevel));
        
    }
    public void OpenOptions()
    {
        optionScreen.SetActive(true);
    }
    public void CloseOptions()
    {
        optionScreen.SetActive(false);
    }
    public void OpenCharacterSelect()
    {
        characterSelectScreen.SetActive(true);
    }
    public void CloseCharacterSelect()
    {
        characterSelectScreen.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }

}
