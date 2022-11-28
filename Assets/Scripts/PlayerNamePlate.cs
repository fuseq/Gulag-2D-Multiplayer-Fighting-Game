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
    public RuntimeAnimatorController newController;
    
    // Update is called once per frame
    void Update()
    {
        string chName=character.photonView.Owner.NickName.Substring(0, character.photonView.Owner.NickName.Length - 1);
        string skin=character.photonView.Owner.NickName.Substring(character.photonView.Owner.NickName.Length - 1);
        usernameText.text =chName ;
        if (skin=="1")
        {
            Animator animator = character.GetComponent<Animator>();
            animator.runtimeAnimatorController = newController;
        }
            
    
    }
}
