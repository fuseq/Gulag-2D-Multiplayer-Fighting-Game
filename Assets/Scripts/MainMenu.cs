using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public string firstLevel;
    public GameObject optionScreen;
    public GameObject chSelect;
    
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
        chSelect.SetActive(true);
    }
    public void CloseCharacterSelect()
    {
        chSelect.SetActive(false);
    }
  
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }

}
