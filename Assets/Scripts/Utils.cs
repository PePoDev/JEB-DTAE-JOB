using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Utils : MonoBehaviour
{

	public void LoadScene(float delay)
	{
		StartCoroutine(Wait());
		IEnumerator Wait()
		{
			yield return new WaitForSeconds(delay / 2);
			Initiate.Fade(1, Color.black, delay);
		}
	}

	public void OpenURL(string url)
	{
		Application.OpenURL(url);
	}
}
