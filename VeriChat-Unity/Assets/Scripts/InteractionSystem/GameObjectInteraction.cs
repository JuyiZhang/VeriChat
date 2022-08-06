using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Object Interaction")]
public class GameObjectInteraction : ScriptableObject
{
    [Tooltip("当您的用户与该物品互动时展示的按钮标题")]
    public string InteractionTitle;
    [Tooltip("当您的用户点击按钮后出现的文字")]
    public string InteractionCaption;
    //[Tooltip("当互动后产生的效果")]
    //public string InteractionEffect;
}
