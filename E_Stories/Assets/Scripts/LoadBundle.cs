using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System;

public class LoadBundle : MonoBehaviour
{
	public DataDialogues SystemDialogue;
	public Transform ContentPanel;

	public IEnumerator Start ()
	{
		print("HI");
		int currentNode = 1;//DialogueSystem.instance.CurrentNode;
		string path = SystemDialogue.Nodes[currentNode].AssetBundeURL;
		string objectName = SystemDialogue.Nodes[currentNode].NameAssetBundleObject;
		if (!String.IsNullOrEmpty(SystemDialogue.Nodes[currentNode].AssetBundeURL))
		{
			string url = path + "/" + SystemDialogue.Nodes[currentNode].TypeLoadingContent.ToString().ToLower();
			WWW www = new WWW(url);
			while (!www.isDone)
			{
				yield return null;
			}
			
			//AssetBundle myasset = www.assetBundle;
			//GameObject bundle = myasset.LoadAsset<GameObject>("AssetBundle");
			//Instantiate(bundle, ContentPanel.transform.position, transform.rotation);
		}
	}
}