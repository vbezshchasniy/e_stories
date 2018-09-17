using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class LoadBundle : MonoBehaviour
{
	public string URL;
	public string ObjectName;
	public DataDialogues SystemDialogue;
	public Image ContentImage;
	public AudioSource AudioSource;
	public VideoPlayer VideoPlayer;

	private void Awake()
	{
		AudioSource = GetComponent<AudioSource>();
		VideoPlayer = GetComponentInChildren<VideoPlayer>();
	}

	private IEnumerator Start () 
	{
		WWW www = new WWW(URL);
		while (!www.isDone)
		{
			yield return null;
		}
		AssetBundle myasset = www.assetBundle;
		switch (SystemDialogue.Nodes[DialogueSystem.instance.CurrentNode].TypeLoadingContent)
		{
			case ContentType.Model3D:
				GameObject object3d = myasset.LoadAsset<GameObject>(ObjectName);
				Instantiate(object3d);
				break;
			case ContentType.Sound:
				AudioClip clip = myasset.LoadAsset<AudioClip>(ObjectName);
				AudioSource.clip = clip;
				break;
			case ContentType.Video:
				VideoClip video = myasset.LoadAsset<VideoClip>(ObjectName);
				VideoPlayer.clip = video;
				break;
			case ContentType.Image:
				Image image = myasset.LoadAsset<Image>(ObjectName);
				ContentImage = image;
				break;
			case ContentType.Empty:
				Debug.Log("Haven't content");
				break;
		}		
	}
}

