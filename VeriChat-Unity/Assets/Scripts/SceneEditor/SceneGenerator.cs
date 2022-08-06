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
    private ItemInstance[] items;
    private string sceneHierarchyFolder;
    private int count = 0;
    private int index = 0;

    [ContextMenu("Generate Room Data")]
    void generateRoomData() {
      CreateFolderIf("Assets","GeneratedScene");
      CreateFolderIf("Assets/GeneratedScene",roomName);
      CreateFolderIf($"Assets/GeneratedScene/{roomName}","sceneHierarchy");
      sceneHierarchyFolder = "Assets/GeneratedScene/" + roomName + "/sceneHierarchy";
      ItemInSceneCompiler currentScene = ScriptableObject.CreateInstance<ItemInSceneCompiler>();
      AssetDatabase.DeleteAsset($"Assets/GeneratedScene/{roomName}/{roomName}_def.asset");
      AssetDatabase.CreateAsset(currentScene, $"Assets/GeneratedScene/{roomName}/{roomName}_def.asset");
      currentScene.title = roomName;
      currentScene.information = roomInfo;
      currentScene.author = authorName;
      currentScene.version = version;
      currentScene.lightDirection = directionalLight.transform.rotation;
      currentScene.lightProperties = directionalLight.GetComponent<Light>();
      currentScene.spawnPosition = spawnPrefab.transform.position;
      currentScene.spawnOrientation = spawnPrefab.transform.rotation;
      dfsCount(sceneHierarchy.transform);
      dfsChild(sceneHierarchy.transform);
      index = 0;
      EditorUtility.ClearProgressBar();
      AssetDatabase.SaveAssets();
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

    void dfsChild(Transform parent) {
      if (parent.childCount == 0 || parent.gameObject.GetComponent<MeshRenderer>() != null || PrefabUtility.GetCorrespondingObjectFromSource(parent.gameObject)) {
        if (File.Exists(sceneHierarchyFolder + "/" + parent.gameObject.name + ".prefab")) { //change position! this only indicates the prefab exists
          return; //Note might need change since we are not sure if the prefab itself is changed is really rare case, better have an alert?
        }
        CreateFolderIf(sceneHierarchyFolder,parent.gameObject.name);
        if (PrefabUtility.GetCorrespondingObjectFromSource(parent.gameObject)) {
          index++;
          EditorUtility.DisplayProgressBar("Generating Scene Hierarchy","Generating Prefab " + parent.gameObject.name, (float)index/(float)count);
          PrefabUtility.SaveAsPrefabAsset(parent.gameObject, sceneHierarchyFolder + "/" + parent.gameObject.name + "/" + parent.gameObject.name + ".prefab");
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
}
