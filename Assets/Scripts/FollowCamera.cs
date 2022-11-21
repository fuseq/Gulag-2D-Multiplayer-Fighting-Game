using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private Vector3 offset;
    private GameObject player;
 
    void Start()
    {
        offset = transform.position;
    }
 
    void LateUpdate()
    {
        if(player == null)
        {
            player =  GameObject.Find("Player(Clone)");
        }
        else
        {
            transform.position = player.transform.position + offset;
        }
    }
}
