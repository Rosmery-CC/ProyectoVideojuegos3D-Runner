using UnityEditor;
using UnityEngine;

public class FixMaterialLocation
{
    [MenuItem("Tools/Actualizar Materiales FBX")]
    public static void FixAllFBX()
    {
        string[] guids = AssetDatabase.FindAssets("t:Model", new[] { "Assets/Polytope Studio" });
        
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            ModelImporter importer = AssetImporter.GetAtPath(path) as ModelImporter;
            
            if (importer != null && importer.materialLocation == ModelImporterMaterialLocation.External)
            {
                importer.materialLocation = ModelImporterMaterialLocation.InPrefab;
                importer.SaveAndReimport();
            }
        }
        Debug.Log("¡Todos los modelos FBX fueron actualizados!");
    }
}
