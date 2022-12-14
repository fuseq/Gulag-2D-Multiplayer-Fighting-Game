using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillBarController : MonoBehaviour
{
    public Image dashImageCooldown;
    public Image attackImageCooldown;
    public float dashCooldown=5f;
    public float attackCooldown=3f;
    private bool dashIsCooldown;
    private bool dashIsPressed = false;
    private bool attackIsCooldown;
    private bool attackIsPressed = false;
    private void Start()
    {
        dashImageCooldown.fillAmount = 1;
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashIsPressed==false)
        {
            dashImageCooldown.fillAmount = 0;
            dashIsCooldown = true;
            dashIsPressed = true;
        }

        if (dashIsCooldown)
        {
            dashImageCooldown.fillAmount += 1 / dashCooldown * Time.deltaTime;
            if (dashImageCooldown.fillAmount>=1)
            {
                dashImageCooldown.fillAmount = 1;
                dashIsCooldown = false;
                dashIsPressed = false;
            }
        }
        
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && attackIsPressed==false)
        {
            attackImageCooldown.fillAmount = 0;
            attackIsCooldown = true;
            attackIsPressed = true;
        }

        if (attackIsCooldown)
        {
            attackImageCooldown.fillAmount += 1 / attackCooldown * Time.deltaTime;
            if (attackImageCooldown.fillAmount>=1)
            {
                attackImageCooldown.fillAmount = 1;
                attackIsCooldown = false;
                attackIsPressed = false;
            }
        }
        
    }
}
