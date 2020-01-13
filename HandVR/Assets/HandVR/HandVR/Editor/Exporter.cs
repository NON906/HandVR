using UnityEngine;
using System.Collections;
using UnityEditor;

public class Exporter
{
    [MenuItem("Assets/ExportWithSettings")]
    static void Export()
    {
        AssetDatabase.ExportPackage(new string[] { "Assets/HandVR", "Assets/Plugins", "Assets/StreamingAssets" },
            "HandVR.unitypackage",
            ExportPackageOptions.Interactive | ExportPackageOptions.Recurse | ExportPackageOptions.IncludeLibraryAssets);
    }
}