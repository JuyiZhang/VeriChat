using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Instance")]
public class ItemInstance : ScriptableObject
{
    public InventoryItemData item;
    public Vector3 position;
    public Quaternion rotation;
}
