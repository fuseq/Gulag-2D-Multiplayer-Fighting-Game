using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class DestroyPowerUp : MonoBehaviour
{
    GameObject imageObject ;
    private Canvas canvas;

    private Image image;
    // Start is called before the first frame update
     void Start()
    {
        imageObject = GameObject.Find("Canvas");
        canvas= imageObject.GetComponent<Canvas>();
        
    }

   
    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.tag == "Player"){
            if (gameObject.tag=="HealItem")
            {
                collision.gameObject.GetComponent<Health>().heal(10);
                Debug.Log(collision.gameObject.GetComponent<Health>().getHeal());
                foreach (Transform eachChild in collision.transform)
                {
                    if (eachChild.name == "pfHealthBar")
                    {
                        eachChild.localScale =new Vector3((collision.gameObject.GetComponent<Health>().getHeal()/100)*1,1,1);
                    }
                }
            }
            if (gameObject.tag=="CooldownItem")
            {
                
                collision.gameObject.GetComponent<CharacterMovement>().resetDashCooldown();
                canvas.GetComponent<SkillBarController>().imageCooldown.fillAmount = 1;
            }
            if (gameObject.tag=="SpeedUpItem")
            {
                collision.gameObject.GetComponent<CharacterMovement>().IncreaseSpeed();
            }
            if (gameObject.tag=="DmgUpItem")
            {
                collision.gameObject.GetComponent<CharacterMovement>().IncreaseSpeed();
            }
           
            GetComponent<PhotonView>().RPC("destroyHealthPack", RpcTarget.AllBuffered);
           
        }
                
    }
    [PunRPC]
    public void destroyHealthPack() 
    {
        Destroy(gameObject);
    }
}
