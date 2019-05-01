using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingTween : MonoBehaviour
{
	public Vector2 TargetPosotion;
	public float Duration;
	public bool StartOnAwkae;

	private void Start()
	{
		if (StartOnAwkae)
		{
			GetComponent<RectTransform>().DOAnchorPos(TargetPosotion, Duration);
			StartCoroutine(WaitTween());
		}
		IEnumerator WaitTween()
		{
			yield return new WaitForSeconds(Duration + 0.5f);
			Initiate.Fade(SceneManager.GetActiveScene().buildIndex + 1, Color.black, Duration);
		} 
	}
}
