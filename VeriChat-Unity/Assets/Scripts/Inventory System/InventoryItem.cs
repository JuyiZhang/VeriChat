using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class InventoryItem
{

    public InventoryItemData data { get; private set; }
    public int stackSize { get; private set; }
    // Start is called before the first frame update
    public InventoryItem(InventoryItemData source) {
      data = source;
      AddToStack();
    }

    public void AddToStack() {
      stackSize++;
    }

    public void RemoveFromStack() {
      stackSize--;
    }
}
