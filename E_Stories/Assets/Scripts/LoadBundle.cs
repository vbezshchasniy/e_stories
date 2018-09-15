using System.Collections;
using UnityEngine;

public class LoadBundle : MonoBehaviour
{
	public string URL;
	public string ObjectName;
	
	private IEnumerator Start () 
	{
		WWW www = new WWW(URL);
		while (!www.isDone)
		{
			yield return null;
		}
		AssetBundle myasset = www.assetBundle;
		GameObject object3d = myasset.LoadAsset<GameObject>(ObjectName);
		Instantiate(object3d);
	}
}