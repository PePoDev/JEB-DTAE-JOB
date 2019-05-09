using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class LoadVideoFromSteamingAssets : MonoBehaviour
{
	public bool PlayOnAwake;
	public string fileName;

	public void Awake()
	{
		var videoPlayer = GetComponent<VideoPlayer>();
		videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, fileName);

		if (PlayOnAwake)
		{
			videoPlayer.Play();
		}
	}
}
