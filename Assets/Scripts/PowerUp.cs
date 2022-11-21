using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using Unity.VisualScripting;

public class PowerUp : MonoBehaviour
{
    private GameObject heal;
    private GameObject speedup;
    public float SpawnTimer = 5f;
    public float DespawnTimer = 3f;
    private GameObject nesne;
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
                nesne = PhotonNetwork.Instantiate("Heal", returnSpawnPoint(), Quaternion.identity, 0, null);
                SpawnTimer = 5f;

            }
        }

    }



    public Vector3 returnSpawnPoint()
    {
        Vector3 sp1=new Vector3(8, 8, 0);
        Vector3 sp2=new Vector3(4, 4, 0);
        Vector3 sp3=new Vector3(-4, -4, 0);
        Vector3 sp4=new Vector3(-8, -8, 0);
        int selectedsp = Random.Range(1, 5);
        if (selectedsp == 1) return sp1;
        if (selectedsp == 2) return sp2;
        if  (selectedsp == 3) return sp3;
         return sp4;
       
    }
}
   
