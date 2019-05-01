using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
	public Image ImageBackgroundFade;
	public Image ImagePopup;
	public Sprite[] popupSprite;
	public Image ImageRecheck;
	public Sprite[] recheckSprite;
	public Image ImageHead;
	public Sprite[] headSprite;
	public Animator phase2_Animator;

	public GameObject phase4;

	private Animator animator;
	private int currentPage;
	private int currentPhase;
	private int currentPopup;

	public object IEnumberator { get; private set; }

	private void Start()
	{
		animator = GetComponent<Animator>();
		currentPage = 1;
		currentPhase = 4;
		currentPopup = 0;
	}

	public void NextPage()
	{
		currentPhase++;
		SwitchPage(currentPhase);
		StartCoroutine(WaitToChangeSprite());
		IEnumerator WaitToChangeSprite()
		{
			yield return new WaitForSeconds(1.5f);
			ChangeRecheck(currentPhase - 4);
		}
	}
	public void SwitchPage(int PageIdx)
	{
		if (currentPage != PageIdx)
		{
			animator.SetInteger("page", PageIdx);
			currentPage = PageIdx;

			var alpha = 1f;
			DOTween.Kill(ImageBackgroundFade);
			switch (PageIdx)
			{
				case 1:
					alpha = 0f;
					break;
			}
			ImageBackgroundFade.DOFade(alpha, 2f);
		}
	}
	public void ColorAlphaFadeIn(Image image)
	{
		image.DOFade(endValue: 1f, duration: 1f);
	}
	public void ColorAlphaFadeOut(Image image)
	{
		image.DOFade(endValue: 0f, duration: 1f);
	}
	public void ChangePopup(int i)
	{
		if (currentPopup + i < 0)
		{
			return;
		}

		if (currentPopup + i == popupSprite.Length)
		{
			ColorAlphaFadeOut(ImagePopup);
			animator.SetInteger("page", 10);
			StartCoroutine(WaitAnimation());
			return;
		}

		currentPopup += i;
		ImagePopup.sprite = popupSprite[currentPopup];

		IEnumerator WaitAnimation()
		{
			yield return new WaitForSeconds(2f);
			ImagePopup.gameObject.SetActive(false);
		}
	}
	public void ChangeRecheck(int i)
	{
		ImageRecheck.overrideSprite = recheckSprite[i];
	}
	public void PlayPhase2()
	{
		phase2_Animator.SetTrigger("new");
	}
	public void PlayPhase4()
	{
		phase4.SetActive(true);
		phase4.GetComponent<VideoPlayer>().Play();
	}
}
