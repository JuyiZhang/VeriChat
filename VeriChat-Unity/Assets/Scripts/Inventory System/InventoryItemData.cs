using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory Item Data")]
public class InventoryItemData : ScriptableObject
{
    public enum styleType {
      Anime,
      Realistic,
      Lowpoly
    };
    public enum itemType {
      Furniture,
      Appliances,
      SmallObject,
      SmallObjectInteractable,
      Food,
      ElectricalGadgets,
      Toy,
      Floor,
      Wall,
      Ceiling,

    };
    public enum sceneType {
      Bedroom,
      Bathroom,
      Kitchen,
      Study,
      DiningRoom,
      LivingRoom,
      Office,
      Lab,
      Balcony
    };
    public string id;
    public string displayName;
    public string theme; // Fantasy, Cyber, Steampunk, etc.
    public styleType style = styleType.Anime;
    public itemType type = itemType.Furniture;
    public sceneType scene = sceneType.Bedroom;
    public Sprite icon;
    public GameObject prefab;
    public GameObjectInteraction[] interactions;
}
