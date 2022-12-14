using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scene Settings")]
public class ItemInSceneCompiler : ScriptableObject
{
  public string title;
  public string information;
  public string author;
  public string version;
  public Vector3 spawnPosition;
  public Quaternion spawnOrientation;
  public List<string> items;
}
