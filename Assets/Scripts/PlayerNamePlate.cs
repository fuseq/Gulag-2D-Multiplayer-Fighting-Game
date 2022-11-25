using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerNamePlate : MonoBehaviour
{
    PhotonView photonView;
    [SerializeField] 
    private TMP_Text usernameText;
    [SerializeField] 
    private CharacterMovement character;
 
    // Update is called once per frame
    void Update()
    {
        usernameText.text = character.photonView.Owner.NickName;
    }
}
