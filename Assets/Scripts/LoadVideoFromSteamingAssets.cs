using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class LoadVideoFromSteamingAssets : MonoBehaviour
{
	public string fileName;
    
	public void Start()
	{
		var videoPlayer = GetComponent<VideoPlayer>();
		videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, fileName + ".mp4");
	}
}
