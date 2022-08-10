using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Instance")]
public class ItemInstance : ScriptableObject
{
    public string item;
    public string fromBundle;
    public Vector3 position;
    public Quaternion rotation;
}
