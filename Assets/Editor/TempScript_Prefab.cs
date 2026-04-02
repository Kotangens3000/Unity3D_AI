using UnityEngine;
using UnityEditor;

public class TempScript_Prefab
{
    [MenuItem("Tools/Force Create Prefab %g")] // Ctrl + G
    private static void Create()
    {
        GameObject go = Selection.activeGameObject;
        if (go == null) return;

        if (!AssetDatabase.IsValidFolder("Assets/Prefabs"))
            AssetDatabase.CreateFolder("Assets", "Prefabs");

        string path = $"Assets/Prefabs/{go.name}.prefab";
        path = AssetDatabase.GenerateUniqueAssetPath(path);

        PrefabUtility.SaveAsPrefabAssetAndConnect(go, path, InteractionMode.AutomatedAction);
        
        AssetDatabase.Refresh();
        Debug.Log("Prefab Created: " + path);
    }
}