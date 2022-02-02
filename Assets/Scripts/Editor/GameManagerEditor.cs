using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[ExecuteInEditMode]
[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Load Data In Editor Mode"))
        {
            GameManager gameManager = (GameManager)target;

            gameManager.LoadGame();
            Debug.Log("Data loaded in editor mode");
        }

        if (GUILayout.Button("Save Data In Editor Mode"))
        {
            GameManager gameManager = (GameManager)target;

            gameManager.SaveGame();
            Debug.Log("Data saved in editor mode");
        }

        GUILayout.EndHorizontal();
    }
}
