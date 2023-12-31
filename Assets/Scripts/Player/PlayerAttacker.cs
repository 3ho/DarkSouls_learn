﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    AnimatorHandler animatorHandler;
    WeaponSlotManager weaponSlotManager;
    InputHandler inputHandler;

    private string lastAttack;

    private void Awake()
    {
        animatorHandler = GetComponentInChildren<AnimatorHandler>();
        weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
        inputHandler = GetComponent<InputHandler>();
    }

    public void HandleWeaponCombo(WeaponItem weapon)
    {
        if(inputHandler.comboFlag)
        {
            animatorHandler.anim.SetBool("canDoCombo", false);

            if (lastAttack == weapon.OH_Light_Attack_01)
            {
                animatorHandler.PlayTargetAnimation(weapon.OH_Light_Attack_02, true);
            }else if(lastAttack == weapon.th_Light_Attack_01)
            {
                animatorHandler.PlayTargetAnimation(weapon.th_Light_Attack_02, true);
            }
        }
    }

    public void HandleLightAttack(WeaponItem weapon)
    {
        weaponSlotManager.attackingWeapon = weapon;
        if (inputHandler.twoHandFlag)
        {
            animatorHandler.PlayTargetAnimation(weapon.th_Light_Attack_01, true);
            lastAttack = weapon.th_Light_Attack_01;
        }
        else
        {
            animatorHandler.PlayTargetAnimation(weapon.OH_Light_Attack_01, true);
            lastAttack = weapon.OH_Light_Attack_01;
        }
    }

    public void HandleHeavyAttack(WeaponItem weapon)
    {
        if(inputHandler.twoHandFlag)
        {

        }
        else
        {
            weaponSlotManager.attackingWeapon = weapon;
            animatorHandler.PlayTargetAnimation(weapon.OH_Heavy_Attack_01, true);
            lastAttack = weapon.OH_Heavy_Attack_01;
        }
    }
}
