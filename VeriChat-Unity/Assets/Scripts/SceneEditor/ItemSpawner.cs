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
    private AssetBundle itemAssetBundle;
    private Dictionary<string,AssetBundle> loadedBundle = new Dictionary<string,AssetBundle>();
    public AssetBundle Bundle;
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
            ItemInstance II = sceneAssetBundle.LoadAsset<ItemInstance>(Is+".asset");
            if(spawnID == II.fromBundle){
              itemAssetBundle = sceneAssetBundle;
            } else {
              if(!loadedBundle.TryGetValue(II.fromBundle, out itemAssetBundle)){
                itemAssetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath+"/"+II.fromBundle);
                loadedBundle.Add(II.fromBundle, itemAssetBundle);
              }
            }
            InventoryItemData IID = itemAssetBundle.LoadAsset<InventoryItemData>(II.item);
            GameObject itemInGame = Instantiate(itemAssetBundle.LoadAsset<GameObject>(IID.prefab));
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
