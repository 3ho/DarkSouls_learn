using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInventorySlot : MonoBehaviour
{
    PlayerInventory playerInventory;
    WeaponSlotManager weaponSlotManager;
    UIManager uiManager;
    public Image icon;
    public WeaponItem item;

    private void Awake()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
        weaponSlotManager = FindObjectOfType<WeaponSlotManager>();
        uiManager = FindObjectOfType<UIManager>();
    }
    public void AddItem(WeaponItem newItem)
    {
        item = newItem;
        icon.sprite = item.itemIcon;
        icon.enabled = true;
        gameObject.SetActive(true);
    }

    public void ClearInventorySlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        gameObject.SetActive(false);
    }

    public void EquipThisItem()
    {
        if(uiManager.rightHandSlot01Select)
        {
            playerInventory.weaponsInventory.Add(playerInventory.weaponInRightHandSlots[0]);
            playerInventory.weaponInRightHandSlots[0] = item;
            playerInventory.weaponsInventory.Remove(item);
        }else if (uiManager.rightHandSlot02Select)
        {
            playerInventory.weaponsInventory.Add(playerInventory.weaponInRightHandSlots[1]);
            playerInventory.weaponInRightHandSlots[1] = item;
            playerInventory.weaponsInventory.Remove(item);
        }
        else if (uiManager.leftHandSlot01Select)
        {
            playerInventory.weaponsInventory.Add(playerInventory.weaponInLeftHandSlots[0]);
            playerInventory.weaponInLeftHandSlots[0] = item;
            playerInventory.weaponsInventory.Remove(item);
        }
        else if (uiManager.leftHandSlot02Select)
        {
            playerInventory.weaponsInventory.Add(playerInventory.weaponInLeftHandSlots[1]);
            playerInventory.weaponInLeftHandSlots[1] = item;
            playerInventory.weaponsInventory.Remove(item);
        }
        else
        {
            return;
        }

        playerInventory.rightWeapon = playerInventory.weaponInRightHandSlots[playerInventory.currentRightWeaponIndex];
        playerInventory.leftWeapon = playerInventory.weaponInLeftHandSlots[playerInventory.currentLeftWeaponIndex];

        weaponSlotManager.LoadWeaponOnSlot(playerInventory.rightWeapon, false);
        weaponSlotManager.LoadWeaponOnSlot(playerInventory.leftWeapon, true);

        uiManager.equipmentWindowUI.LoadWeaponsOnEquipmentScreen(playerInventory);
        uiManager.ResetAllSelectedSlots();
    }
}
