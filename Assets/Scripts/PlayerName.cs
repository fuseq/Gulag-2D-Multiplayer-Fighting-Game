using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class PlayerName : MonoBehaviour
{
    public TMP_InputField nametf;
    public Button setNameBtn;
    public GameObject nameSelectScreen;
    public string username;

    
    // Update is called once per frame
    void Update()
    {
        OnTFChange();
    }

    public void OnTFChange()
    {
        if (nametf.text.Length >= 3)
            setNameBtn.interactable = true;
        else
            setNameBtn.interactable = false;
    }


    public void CloseNameSelect()
    {
        nameSelectScreen.SetActive(false);
       
        PhotonNetwork.NickName = nametf.text+CharacterManager.selectedOption;
        
        
        Debug.Log(PhotonNetwork.NickName);
        
    }

    
    
}