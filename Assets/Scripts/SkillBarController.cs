using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillBarController : MonoBehaviour
{
    public Image imageCooldown;
    public float cooldown=5f;
    private bool isCooldown;
    private bool isPressed = false;
    private void Start()
    {
        imageCooldown.fillAmount = 1;
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && isPressed==false)
        {
            imageCooldown.fillAmount = 0;
            isCooldown = true;
            isPressed = true;
        }

        if (isCooldown)
        {
            imageCooldown.fillAmount += 1 / cooldown * Time.deltaTime;
            if (imageCooldown.fillAmount>=1)
            {
                imageCooldown.fillAmount = 1;
                isCooldown = false;
                isPressed = false;
            }
        }
    }
}
