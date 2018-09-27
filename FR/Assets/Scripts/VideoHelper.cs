using UnityEngine;
using UnityEngine.Video;

public class VideoHelper : MonoBehaviour
{
	private VideoPlayer VideoPlayer;
	private void Start ()
	{
		VideoPlayer = GetComponent<VideoPlayer>();
		if (VideoPlayer.targetCamera == null)
		{
			VideoPlayer.targetCamera = Camera.main;
		}
	}
}
