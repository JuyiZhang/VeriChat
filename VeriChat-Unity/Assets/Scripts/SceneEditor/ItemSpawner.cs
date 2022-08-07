using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private bool isEditMode;
    [SerializeField] private GameObject editCanvasPrefab;
    [SerializeField] private ItemInSceneCompiler ItemsInScene;
    [SerializeField] private GameObject itemRoot;
    public string spawnID;
    private AssetBundle sceneAssetBundle;
    // Start is called before the first frame update
    void Start()
    {
        if (spawnID != null) {
          sceneAssetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath+"/"+spawnID);
          ItemsInScene = sceneAssetBundle.LoadAsset<ItemInSceneCompiler>(spawnID+"_def");
        } else {
          sceneAssetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath+"/"+ItemsInScene.author+ItemsInScene.title);
        }
        foreach (string Is in ItemsInScene.items) {
            ItemInstance II = sceneAssetBundle.LoadAsset<ItemInstance>(Is);
            InventoryItemData IID = sceneAssetBundle.LoadAsset<InventoryItemData>(II.item);
            GameObject itemInGame = Instantiate(sceneAssetBundle.LoadAsset<GameObject>(IID.prefab));
            itemInGame.transform.SetParent(itemRoot.transform);
            itemInGame.transform.position = II.position;
            itemInGame.transform.rotation = II.rotation;
            int itemLayer = LayerMask.NameToLayer("Item");
            itemInGame.layer = itemLayer;
            Debug.Log("Item Spawned");
            ItemObject itemObjectComponent = itemInGame.AddComponent(typeof(ItemObject)) as ItemObject;
            itemObjectComponent.referenceItem = IID;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
