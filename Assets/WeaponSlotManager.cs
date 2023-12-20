using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlotManager : MonoBehaviour
{
    WeaponHodlerSlot leftHandSlot;
    WeaponHodlerSlot rightHandSlot;

    DamageCollider leftHandDamageCollider;
    DamageCollider rightHandDamageCollider;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        WeaponHodlerSlot[] weaponHodlerSlots = GetComponentsInChildren<WeaponHodlerSlot>();
        foreach (WeaponHodlerSlot weaponSlot in weaponHodlerSlots)
        {
            if (weaponSlot.isLeftHandSlot)
            {
                leftHandSlot = weaponSlot;
            }
            else if (weaponSlot.isRightHandSlot)
            {
                rightHandSlot = weaponSlot;
            }
        }
    }

    public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
    {
        if (isLeft)
        {
            leftHandSlot.LoadWeaponModel(weaponItem);
            LoadLeftWeaponDamageCollider();

            if(weaponItem != null)
            {
                animator.CrossFade(weaponItem.left_hand_idle, 0.2f);
            }
            else
            {
                animator.CrossFade("Left Arm Empty", 0.2f);
            }
        }
        else
        {
            rightHandSlot.LoadWeaponModel(weaponItem);
            LoadRightWeaponDamageCollider();

            if (weaponItem != null)
            {
                animator.CrossFade(weaponItem.right_hand_idle, 0.2f);
            }
            else
            {
                animator.CrossFade("Right Arm Empty", 0.2f);
            }
        }
    }

    private void LoadLeftWeaponDamageCollider()
    {
        leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
    }

    private void LoadRightWeaponDamageCollider()
    {
        rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
    }

    public void OpenRightDamageCollider()
    {
        rightHandDamageCollider.EnableDamageCollider();
    }

    public void OpenLeftDamageCollider()
    {
        leftHandDamageCollider.EnableDamageCollider();
    }

    public void CloseRightDamageCollider()
    {
        rightHandDamageCollider.DisableDamageCollider();
    }

    public void CloseLeftDamageCollider()
    {
        leftHandDamageCollider.DisableDamageCollider();
    }
}
