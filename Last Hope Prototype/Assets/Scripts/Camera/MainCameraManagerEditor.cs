#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MainCameraManager))]
public class MainCameraManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MainCameraManager myScript = (MainCameraManager)target;
        if (GUILayout.Button("Swap camera mode"))
        {
            myScript.SwapCameraMode();
        }
    }
}
#endif