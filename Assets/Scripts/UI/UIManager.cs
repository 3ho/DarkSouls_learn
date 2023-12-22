using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public EquipmentWindowUI equipmentWindowUI;

    [Header("UI Windows")]
    public GameObject hudWindow;
    public GameObject selectWindow;
    public GameObject equipmentScreenWindow;
    public GameObject weaponInventoryWindow;

    [Header("Equipment Window Slot Selected")]
    public bool rightHandSlot01Select;
    public bool rightHandSlot02Select;
    public bool leftHandSlot01Select;
    public bool leftHandSlot02Select;

    [Header("Weapon Inventory")]
    public GameObject WeaponInventorySlotPerfab;
    public Transform weaponInventroySlotParent;
    WeaponInventorySlot[] weaponInventorySlots;

    private void Awake()
    {
        //equipmentWindowUI = FindObjectOfType<EquipmentWindowUI>();
    }

    private void Start()
    {
        weaponInventorySlots = weaponInventroySlotParent.GetComponentsInChildren<WeaponInventorySlot>();
        equipmentWindowUI.LoadWeaponsOnEquipmentScreen(playerInventory);
    }

    public void UpdateUI()
    {
        for(int i=0;i<weaponInventorySlots.Length;i++)
        {
            if(i < playerInventory.weaponsInventory.Count)
            {
                if(weaponInventorySlots.Length < playerInventory.weaponsInventory.Count)
                {
                    Instantiate(WeaponInventorySlotPerfab, weaponInventroySlotParent);
                    weaponInventorySlots = weaponInventroySlotParent.GetComponentsInChildren<WeaponInventorySlot>();
                }
                weaponInventorySlots[i].AddItem(playerInventory.weaponsInventory[i]);
            }
            else
            {
                weaponInventorySlots[i].ClearInventorySlot();
            }
        }
    }

    public void OpenSelectWindow()
    {
        selectWindow.SetActive(true);
    }

    public void CloseSelectWindow()
    {
        selectWindow.SetActive(false);
    }

    public void CloseAllInventoryWindows()
    {
        ResetAllSelectedSlots();
        weaponInventoryWindow.SetActive(false);
        equipmentScreenWindow.SetActive(false);
    }

    public void ResetAllSelectedSlots()
    {
        rightHandSlot01Select = false;
        rightHandSlot02Select = false;
        leftHandSlot01Select = false;
        leftHandSlot02Select = false;
    }
}
