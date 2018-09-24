using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class WTVideoPlayer : MonoBehaviour 
{
	public VideoClip VideoToPlay;
    public bool IsVideoServer;
    public string VideoUrl;
    private RawImage RawImg;
    private VideoPlayer VideoPlayer;
    private VideoSource VideoSource;
    private AudioSource AudioSource;
	
	private void Start ()
	{
	    RawImg = GetComponent<RawImage>();
        Application.runInBackground = true;
        StartCoroutine(PlayVideo());
	}
 
    private IEnumerator PlayVideo()
    {
        VideoPlayer = gameObject.AddComponent<VideoPlayer>();
        AudioSource = gameObject.AddComponent<AudioSource>();
        VideoPlayer.playOnAwake = false;
        AudioSource.playOnAwake = false;
        AudioSource.Pause();
        if (IsVideoServer && VideoUrl != null)
        {
            VideoPlayer.source = VideoSource.Url;
            VideoPlayer.url = VideoUrl; 
        }
        else
        {
            VideoPlayer.source = VideoSource.VideoClip;   
        }
        VideoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        VideoPlayer.EnableAudioTrack(0, true);
        VideoPlayer.SetTargetAudioSource(0, AudioSource); 
        VideoPlayer.clip = VideoToPlay;
        VideoPlayer.Prepare(); 
        WaitForSeconds waitTime = new WaitForSeconds(1);
        while (!VideoPlayer.isPrepared)
        {
            Debug.Log("Preparing Video");
            yield return waitTime;
            break;
        }
        Debug.Log("Done Preparing Video");
        RawImg.texture = VideoPlayer.texture;
        VideoPlayer.Play();
        AudioSource.Play();
        Debug.Log("Playing Video");
        while (VideoPlayer.isPlaying)
        {
            Debug.LogWarning("Video Time: " + Mathf.FloorToInt((float)VideoPlayer.time));
            yield return null;
        }
        Debug.Log("Done Playing Video");
    }
}