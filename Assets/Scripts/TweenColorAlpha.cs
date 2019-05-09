using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TweenColorAlpha : MonoBehaviour
{
	public float speed;

	private float alpha = 0f;
	private bool isFadeIn = true;
	private Image image;

	void Start()
	{
		image = GetComponent<Image>();
		image.color = new Color(1f, 1f, 1f, alpha);
	}

	void Update()
	{
		if (isFadeIn)
		{
			alpha += speed * Time.deltaTime;
			if (alpha > 1f)
			{
				isFadeIn = false;
			}
		}
		else
		{
			alpha -= speed * Time.deltaTime;
			if (alpha < 0f)
			{
				isFadeIn = true;
			}
		}
		image.color = new Color(1f, 1f, 1f, alpha);
	}
}
