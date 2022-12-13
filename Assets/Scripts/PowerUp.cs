using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using TMPro;
using Unity.VisualScripting;

public class PowerUp : MonoBehaviour
{
    private GameObject heal;
    private GameObject speedup;
    public float SpawnTimer = 20f;
    private GameObject nesne;
    private int[] spawnLoc = new int[4];
    void Start()
    {
        
        int randpower = Random.Range(1, 3);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        if (PhotonNetwork.IsConnected)
        {
            SpawnTimer -= Time.deltaTime;
            if (SpawnTimer <= 0f)
            {
                nesne = PhotonNetwork.Instantiate(itemType(), returnSpawnPoint(), Quaternion.identity, 0, null);
                SpawnTimer = 20f;
                
            }

        }

    }



    public Vector3 returnSpawnPoint()
    {
        Vector3 sp1=new Vector3(-4, -4, 0);
        Vector3 sp2=new Vector3(4, 4, 0);
        Vector3 sp3=new Vector3(4, -4, 0);
        Vector3 sp4=new Vector3(-4, 4, 0);
        int selectedsp = Random.Range(1, 5);
        if (selectedsp == 1)
        
            return sp1;
       
       else if (selectedsp == 2 )
        
            return sp2;
        
       else if (selectedsp == 3)
        
            return sp3;
         
        else
            return sp4;




    }

    public string itemType()
    {
        string heal = "Heal";
        string cooldown = "Cooldown";
        string speedup = "Speed";
        int selecteditem = Random.Range(1, 4);
        if (selecteditem == 1) return heal;
        if (selecteditem == 2) return cooldown;
        return speedup;
        
    }
}
   
