using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class SceneGenerator : MonoBehaviour
{
    [SerializeField] private GameObject sceneHierarchy;
    [SerializeField] private GameObject spawnPrefab;
    [SerializeField] private GameObject directionalLight;
    [SerializeField] private string roomName;
    [SerializeField] private string roomInfo;
    [SerializeField] private string authorName;
    [SerializeField] private string version;
    private List<string> items = new List<string>();
    private string sceneHierarchyFolder;
    private int count = 0;
    private int index = 0;
    private AssetBundle sceneAssetBundle;

#if UNITY_EDITOR
    [ContextMenu("Generate Room Data")]
    void generateRoomData() {
      items.Clear();
      CreateFolderIf("Assets","GeneratedScene");
      CreateFolderIf("Assets/GeneratedScene",roomName);
      CreateFolderIf("Assets/GeneratedScene","AssetBundle");
      CreateFolderIf($"Assets/GeneratedScene/{roomName}","sceneHierarchy");
      CreateFolderIf($"Assets/GeneratedScene/{roomName}/sceneHierarchy","Items");
      sceneHierarchyFolder = "Assets/GeneratedScene/" + roomName + "/sceneHierarchy";
      ItemInSceneCompiler currentScene = ScriptableObject.CreateInstance<ItemInSceneCompiler>();
      AssetDatabase.DeleteAsset($"Assets/GeneratedScene/{roomName}/{authorName}{roomName}_def.asset");
      currentScene.title = roomName;
      currentScene.information = roomInfo;
      currentScene.author = authorName;
      currentScene.version = version;
      currentScene.spawnPosition = spawnPrefab.transform.position;
      currentScene.spawnOrientation = spawnPrefab.transform.rotation;
      dfsCount(sceneHierarchy.transform);
      dfsChild(sceneHierarchy.transform);
      index = 0;
      EditorUtility.ClearProgressBar();
      dfsItem(sceneHierarchy.transform);
      index = 0;
      currentScene.items = items;
      Debug.Log(currentScene);
      AssetDatabase.CreateAsset(currentScene, $"Assets/GeneratedScene/{roomName}/{authorName}{roomName}_def.asset");
      var importer = AssetImporter.GetAtPath($"Assets/GeneratedScene/{roomName}/{authorName}{roomName}_def.asset");
      importer.assetBundleName = authorName + roomName;
      if(EditorUtility.DisplayDialog("Scene Successfully Created","You have successfully generated the file required for the scene to work. Would you like to pack them to an Asset Bundle for publish?","Yes","No")){
        BuildPipeline.BuildAssetBundles("Assets/GeneratedScene/AssetBundle",BuildAssetBundleOptions.None,BuildTarget.StandaloneOSX); // Change this!
      }
    }

    void dfsCount(Transform parent) {
      if (parent.childCount == 0 || parent.gameObject.GetComponent<MeshRenderer>() != null || PrefabUtility.GetCorrespondingObjectFromSource(parent.gameObject)) {
        count++;
        return;
      }
      foreach (Transform child in parent) {
        dfsCount(child);
      }
    }

    void dfsItem(Transform parent) {
      if (parent.childCount == 0 || parent.gameObject.GetComponent<MeshRenderer>() != null || PrefabUtility.GetCorrespondingObjectFromSource(parent.gameObject)) {
        //Create Item ItemInstance
        if (parent.gameObject.GetComponent<ItemObject>() != null) {
          var newInventory = Instantiate(parent.gameObject.GetComponent<ItemObject>().referenceItem);
          AssetDatabase.DeleteAsset(sceneHierarchyFolder + "/" + parent.gameObject.name + "/" + parent.gameObject.name + ".asset");
          AssetDatabase.CreateAsset(newInventory, sceneHierarchyFolder + "/" + parent.gameObject.name + "/" + parent.gameObject.name + ".asset");
        } else {
          AssetDatabase.DeleteAsset(sceneHierarchyFolder + "/" + parent.gameObject.name + "/" + parent.gameObject.name + ".asset");
          InventoryItemData newInventory = ScriptableObject.CreateInstance<InventoryItemData>();
          newInventory.id = roomName + parent.gameObject.name;
          newInventory.displayName = parent.gameObject.name;
          newInventory.prefab = parent.gameObject.name + ".prefab";//sceneAssetBundle.LoadAsset<GameObject>(parent.gameObject.name);
          AssetDatabase.CreateAsset(newInventory, sceneHierarchyFolder + "/" + parent.gameObject.name + "/" + parent.gameObject.name + ".asset");
          ItemInstance newItem = ScriptableObject.CreateInstance<ItemInstance>();
          newItem.item = parent.gameObject.name + ".asset";
          newItem.position = parent.position;
          newItem.rotation = parent.rotation;
          AssetDatabase.CreateAsset(newItem, sceneHierarchyFolder + "/Items/" + parent.gameObject.name + "_item.asset");
          items.Add(parent.gameObject.name + "_item.asset");
          //Debug.Log("Asset Created");
          var importer = AssetImporter.GetAtPath(sceneHierarchyFolder + "/" + parent.gameObject.name + "/" + parent.gameObject.name + ".asset");
          importer.assetBundleName = authorName + roomName;
          var importer2 = AssetImporter.GetAtPath(sceneHierarchyFolder + "/Items/" + parent.gameObject.name + "_item.asset");
          importer2.assetBundleName = authorName + roomName;
        }

        return;
      }
      foreach (Transform child in parent) {
        dfsItem(child);
      }
    }

    void dfsChild(Transform parent) {
      if (parent.childCount == 0 || parent.gameObject.GetComponent<MeshRenderer>() != null || PrefabUtility.GetCorrespondingObjectFromSource(parent.gameObject)) {
        CreateFolderIf(sceneHierarchyFolder,parent.gameObject.name);
        index++;
        EditorUtility.DisplayProgressBar("Generating Scene Hierarchy","Generating Prefab " + parent.gameObject.name, (float)index/(float)count);
        if (File.Exists(sceneHierarchyFolder + "/" + parent.gameObject.name + ".prefab")) { //change position! this only indicates the prefab exists
          //return; Note might need change since we are not sure if the prefab itself is changed is really rare case, better have an alert?
        } else {
          //Save Prefab
          GameObject inventoryPrefab = PrefabUtility.SaveAsPrefabAsset(parent.gameObject, sceneHierarchyFolder + "/" + parent.gameObject.name + "/" + parent.gameObject.name + ".prefab");
          var importer = AssetImporter.GetAtPath(sceneHierarchyFolder + "/" + parent.gameObject.name + "/" + parent.gameObject.name + ".prefab");
          importer.assetBundleName = authorName + roomName;
        }
        return;
      }
      foreach (Transform child in parent) {
        dfsChild(child);
      }
    }

    void CreateFolderIf(string parent, string folderName) {
      if(!AssetDatabase.IsValidFolder($"{parent}/{folderName}")){
        AssetDatabase.CreateFolder(parent,folderName);
      }
    }
#endif
}
