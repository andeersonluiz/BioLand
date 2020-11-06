
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class exportProject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string[] projectContent = AssetDatabase.GetAllAssetPaths();  
        AssetDatabase.ExportPackage(projectContent, "UltimateTemplate.unitypackage", ExportPackageOptions.Recurse | ExportPackageOptions.IncludeLibraryAssets );  
        Debug.Log("Project Exported"); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
#endif
