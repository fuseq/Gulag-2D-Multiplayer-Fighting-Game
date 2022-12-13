using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using Photon.Voice.PUN;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using Hashtable=ExitGames.Client.Photon.Hashtable;
public class ManageOnline : MonoBehaviourPunCallbacks
{
    public int playerCount;
    Vector3 sp1=new Vector3(-11.84f, 0.6f, 0);
    Vector3 sp2=new Vector3(11.84f,  0.6f, 0);
    void Start()
    {
       
        
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        
        print(PhotonNetwork.LocalPlayer.NickName);
        PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        print("Disconnected from server for reason"+ cause.ToString());
        
    }
   public override void OnJoinedLobby()
    {
        
        PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions { MaxPlayers = 2, IsOpen = true, IsVisible = true },TypedLobby.Default);
        
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("joined to room");
        playerCounter();
        if (PhotonNetwork.PlayerList.Length==1)
        {
            GameObject nesne = PhotonNetwork.Instantiate("Player", sp1, Quaternion.identity, 0, null);
        }
        else
        {
            GameObject nesne = PhotonNetwork.Instantiate("Player", sp2, Quaternion.identity, 0, null); 
        }
        
        
      
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
       
        Debug.Log("Other players joined the room.");
        playerCounter();
    }

  
    
    public void DisconnectPlayer()
    {
        
        StartCoroutine(DisconnectAndLoad());
    }
 
    IEnumerator DisconnectAndLoad()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.AutomaticallySyncScene = false;
        }
        else
            yield return null;
       
        PhotonNetwork.Disconnect();
        
   
    }


 
    public override void OnLeftLobby()
    {
        Debug.Log("left to lobby");
    }
    public override void OnLeftRoom()
    {
        Debug.Log("left to room");
        
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Error: entering room");
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Error: entering any room");
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Error: creating room");
    }

    public void playerCounter()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
        {
            playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

        }
    }
  

  
}
