using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CustomEditor(typeof(MainCameraManager))]
public class MainCameraManagerEditor : Editor
{
    #if UNITY_EDITOR
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MainCameraManager myScript = (MainCameraManager)target;
        if (GUILayout.Button("Swap camera mode"))
        {
            myScript.SwapCameraMode();
        }
    }
    #endif
}