using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public Image BackgroundFade;

	private Animator animator;
	private int currentPage;

	private void Start()
	{
		animator = GetComponent<Animator>();
		currentPage = 1;
	}

	private void Update()
	{

	}
	public void SwitchPage(int PageIdx)
	{
		if (currentPage != PageIdx)
		{
			animator.SetInteger("page", PageIdx);
			currentPage = PageIdx;

			var alpha = 0f;
			DOTween.Kill(BackgroundFade);
			switch (PageIdx)
			{
				case 1:
					alpha = 0f;
					break;
				case 2:
				case 3:
					alpha = 1f;
					break;
			}
			BackgroundFade.DOFade(alpha, 2f);
		}
	}
}
