using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "CreateLevel", order = 1)]
public class LevelScriptable : ScriptableObject
{
    public int levelId;
    public GameObject levelPrefab;
    public string description;
}
