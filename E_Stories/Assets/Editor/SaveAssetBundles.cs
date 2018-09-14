using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SaveAssetBundles : EditorWindow {

	[MenuItem("Window/Save AssetBundle")]
	public static void Open ()
	{
		BuildPipeline.BuildAssetBundles("Assets/AssetsBundles", BuildAssetBundleOptions.None, BuildTarget.Android);
		AssetDatabase.Refresh();
	}

	[MenuItem("Window/Save Specific AssetBundle")]
	public static void SaveSpecificAssets()
	{
		AssetBundleBuild build = new AssetBundleBuild();
		build.assetBundleName = "objTest";
		string[] files = AssetDatabase.GetAllAssetPaths();
		List<string> validFiles = new List<string>();
		foreach (var file in files)
		{
			if (file.EndsWith(".prefab"))
			{
				validFiles.Add(file);
			}
			
		}

		build.assetNames = validFiles.ToArray();
		BuildPipeline.BuildAssetBundles("Assets/AssetsBundles", new AssetBundleBuild[] {build}, BuildAssetBundleOptions.None, BuildTarget.Android);
	}
}
