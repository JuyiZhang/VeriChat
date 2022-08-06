using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public InventoryItemData referenceItem;
    // Start is called before the first frame update
    public void OnHandlePickupItem() {
      Debug.Log(referenceItem);

      InventorySystem.current.Add(referenceItem);
      Destroy(gameObject);
    }
}
