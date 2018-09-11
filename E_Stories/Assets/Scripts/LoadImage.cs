using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadImage : MonoBehaviour
{
	public bool IsServer;
	[SerializeField]
	private string ServerURL;
	[SerializeField]
	private string LocalPath;
	private WWW www;
	
	private void Start ()
	{
		if (IsServer)
		{
			SendStringState(ServerURL, "Server URL is empty");
			StartCoroutine(LoadImageFromServer());
		}
		else
		{
			SendStringState(LocalPath, "Local path is empty");
			StartCoroutine(LoadImageFromLocalPath());
		}
		
	}

	private void SendStringState(string purpose, string sendText)
	{
		if (string.IsNullOrEmpty(purpose))
		{
			Debug.Log(sendText);
		}
	}

	private IEnumerator LoadImageFromLocalPath()
	{
		www = new WWW(LocalPath);
		yield return www;
		SetSprite();
	}

	private IEnumerator LoadImageFromServer()
	{
		if (Application.internetReachability == NetworkReachability.NotReachable)
		{
			Debug.Log("Network not reachable");
			yield return null;
		}
		www = new WWW(ServerURL);
		Debug.Log("Download image on progress");
		yield return www;
		if (string.IsNullOrEmpty(www.text))
		{
			Debug.Log("Download is failed");
		}
		else
		{
			Debug.Log("Download is success");
			SetSprite();
		}
	}

	private void SetSprite()
	{
		Texture2D texture2D = new Texture2D(1, 1);
		www.LoadImageIntoTexture(texture2D);
		Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), Vector2.one / 2);
		GetComponent<Image>().sprite = sprite;
	}
}