using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadObjectFromBundle : MonoBehaviour
{
	private string Path;
	
	private void Start()
	{
		Path = "file:///";
		Path += Application.dataPath;
		Path += "/Prefabs";

//		Path = "file:///E://workspace/E_story/E_Stories/Assets/Prefabs";
		StartCoroutine(DownloadObject());
	}

	IEnumerator DownloadObject()
	{
		WWW www = WWW.LoadFromCacheOrDownload(Path, 1);
		Debug.Log(Path);
		yield return www;

		
		
		AssetBundle bundle = www.assetBundle;
//		Debug.Log(bundle.name);
		AssetBundleRequest request = bundle.LoadAssetAsync<GameObject>("Button");
		Debug.Log(request);
		yield return request;
		
		GameObject obj = request.asset as GameObject;
		Instantiate<GameObject>(obj);
	}
}
