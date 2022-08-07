using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory Item Data")]
public class InventoryItemData : ScriptableObject
{
    public enum styleType {
      Unspecified,
      Anime,
      Realistic,
      Lowpoly
    };
    public enum itemType {
      Unspecified,
      Furniture,
      Appliances,
      SmallObject,
      SmallObjectInteractable,
      Food,
      ElectricalGadgets,
      Toy,
      Floor,
      Wall,
      Ceiling
    };
    public enum sceneType {
      Unspecified,
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
    public string icon;
    public string prefab;
    public GameObjectInteraction[] interactions;
}
