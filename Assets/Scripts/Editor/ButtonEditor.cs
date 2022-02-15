using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[ExecuteInEditMode]
[CustomEditor(typeof(ButtonController))]
public class ButtonEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ButtonController buttonController = (ButtonController)target;

        GUILayout.Space(5);

        if (!buttonController.isUI)
        {
            buttonController.targetLevel = (Level)EditorGUILayout.EnumPopup("Choose Target Level", buttonController.targetLevel);
        }
        else
        {
            buttonController.canvasType= (CanvasType)EditorGUILayout.EnumPopup("Choose Target UI Canvas",buttonController.canvasType);
        }
    }
}
