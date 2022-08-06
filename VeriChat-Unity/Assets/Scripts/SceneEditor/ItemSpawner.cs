using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private bool isEditMode;
    [SerializeField] private GameObject editCanvasPrefab;
    [SerializeField] private ItemInSceneCompiler ItemsInScene;
    [SerializeField] private GameObject itemRoot;
    // Start is called before the first frame update
    void Start()
    {
        foreach (ItemInstance II in ItemsInScene.items) {
            GameObject itemInGame = Instantiate(II.item.prefab);
            itemInGame.transform.SetParent(itemRoot.transform);
            itemInGame.transform.position = II.position;
            itemInGame.transform.rotation = II.rotation;
            int itemLayer = LayerMask.NameToLayer("Item");
            itemInGame.layer = itemLayer;
            Debug.Log("Item Spawned");
            ItemObject itemObjectComponent = itemInGame.AddComponent(typeof(ItemObject)) as ItemObject;
            itemObjectComponent.referenceItem = II.item;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
