using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class DestroyPowerUp : MonoBehaviour
{
    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.tag == "Player"){
            GetComponent<PhotonView>().RPC("destroyHealthPack", RpcTarget.AllBuffered);
        }
                
    }
    [PunRPC]
    public void destroyHealthPack() 
    {
        Destroy(gameObject);
    }
}
