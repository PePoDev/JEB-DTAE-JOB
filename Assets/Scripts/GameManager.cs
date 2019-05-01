using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public Image BackgroundFade;

	private Animator animator;

	private void Start()
	{
		animator = GetComponent<Animator>();
	}

	private void Update()
	{

	}
	public void SwitchPage(int PageIdx)
	{
		DOTween.Kill(BackgroundFade);
		animator.SetInteger("page", PageIdx);
	}
	public void FadeBackground(float alpha)
	{
		BackgroundFade.DOFade(alpha, 2f);
	}
}
