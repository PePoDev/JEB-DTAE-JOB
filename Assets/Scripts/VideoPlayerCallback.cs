using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class VideoPlayerCallback : MonoBehaviour
{
	public UnityEvent OnVideoEnd;

	private bool isPlaying = false;
	private VideoPlayer videoPlayer;

	private void Start()
	{
		videoPlayer = GetComponent<VideoPlayer>();
	}

	private void Update()
	{
		if (!isPlaying && videoPlayer.isPlaying)
		{
			isPlaying = true;
		}

		if (isPlaying)
		{
			if (!videoPlayer.isPlaying)
			{
				if (OnVideoEnd != null)
				{
					isPlaying = false;
					OnVideoEnd.Invoke();
				}
			}
		}
	}
}
