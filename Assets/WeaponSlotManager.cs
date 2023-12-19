using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlotManager : MonoBehaviour
{
    WeaponHodlerSlot leftHandSlot;
    WeaponHodlerSlot rightHandSlot;

    private void Awake()
    {
        WeaponHodlerSlot[] weaponHodlerSlots = GetComponentsInChildren<WeaponHodlerSlot>();
        foreach(WeaponHodlerSlot weaponSlot in weaponHodlerSlots)
        {
            if(weaponSlot.isLeftHandSlot)
            {
                leftHandSlot = weaponSlot;
            }else if(weaponSlot.isRightHandSlot)
            {
                rightHandSlot = weaponSlot;
            }
        }
    }

    public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
    {
        if(isLeft)
        {
            leftHandSlot.LoadWeaponModel(weaponItem);
        }
        else
        {
            rightHandSlot.LoadWeaponModel(weaponItem);
        }
    }
}
