using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public string mainMenu;
    private GameObject player;
    
    public void LoadMenu()
    {
        
        SceneManager.LoadScene((mainMenu));
    }
   
    
    
}