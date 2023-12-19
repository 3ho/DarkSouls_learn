using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    [Header("Item Information")]
    public Sprite itemIcon;
    public string itemName;

    [Header("One Handed Attack Animations")]
    public string OH_Light_Attack_01;
    public string OH_Heavy_Attack_01;
}
