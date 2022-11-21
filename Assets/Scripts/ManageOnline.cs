using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class ManageOnline : MonoBehaviourPunCallbacks
{
    
    void Start()
    {
        print("Connecting to Server");
        
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print("connected to server");
        print(PhotonNetwork.LocalPlayer.NickName);
        PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        print("Disconnected from server for reason"+ cause.ToString());
        
    }
   public override void OnJoinedLobby()
    {
        Debug.Log("joined to lobby");
        PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions { MaxPlayers = 2, IsOpen = true, IsVisible = true },TypedLobby.Default);
        
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("joined to room");
        GameObject nesne = PhotonNetwork.Instantiate("Player", returnSpawnPoint(), Quaternion.identity, 0, null);
      
    }

    public Vector3 returnSpawnPoint()
    {
       Vector3 sp1=new Vector3(-11.84f, 6.86f, 0);
       Vector3 sp2=new Vector3(-11.84f, -6.86f, 0);
       Vector3 sp3=new Vector3(11.84f, 6.86f, 0);
       Vector3 sp4=new Vector3(11.84f, -6.86f, 0);
       int selectedsp = Random.Range(1, 5);
       if (selectedsp == 1) return sp1;
       else if (selectedsp == 2) return sp2;
       else if  (selectedsp == 3) return sp3;
       else return sp4;
       
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
        print("bomba");
   
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

  
}
