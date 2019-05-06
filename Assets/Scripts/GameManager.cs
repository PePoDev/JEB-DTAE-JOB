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

    public GameObject phase2_Animator_obj;
    private Animator phase2_Animator;

    public GameObject phase4;
    public GameObject arrowOnVideo;

    public Image phase2_head;
    public Image phase2_detail;

    public Sprite[] Phase2_DetailPopup;
    public Sprite[] Phase2_Head;
    public AudioSource[] phase2_audio_head;

    public VideoPlayer phase3_videoPlayer;
    public Toggle[] checkListResult;
    public GameObject arrowOnVideoPhase3;

    private Animator animator;
    private int currentPage;
    private int currentPhase;
    private int currentPopup;
    private int currentHead;

    private void Start()
    {
        animator = GetComponent<Animator>();
        phase2_Animator = phase2_Animator_obj.GetComponent<Animator>();
        currentPage = 1;
        currentPhase = 4;
        currentPopup = 0;
        currentHead = -1;
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
    public void ChangePopup(int i)
    {
        if (currentPopup + i < 0)
        {
            return;
        }

        if (currentPopup + i == popupSprite.Length)
        {
            animator.SetInteger("page", 10);
            currentPopup = 0;
            StartCoroutine(WaitAnimation());
            return;
        }

        currentPopup += i;
        ImagePopup.overrideSprite = popupSprite[currentPopup];

        IEnumerator WaitAnimation()
        {
            yield return new WaitForSeconds(2f);
            ImagePopup.color = new Color(1f, 1f, 1f, 0f);
            ImagePopup.SetAllDirty();
            ImagePopup.gameObject.SetActive(false);
        }
    }
    public void ChangeRecheck(int i)
    {
        ImageRecheck.overrideSprite = recheckSprite[i];
    }
    public void PlayPhase4()
    {
        phase4.SetActive(true);
        arrowOnVideo.SetActive(true);
        VideoPlayer videoPlayer = phase4.GetComponent<VideoPlayer>();
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "v.mp4");
        videoPlayer.Prepare();

        videoPlayer.prepareCompleted += (vp) =>
        {
            vp.targetMaterialRenderer.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            vp.Play();
        };
    }
    public void setColorToTransparent(SpriteRenderer sprite)
    {
        sprite.color = new Color(1f, 1f, 1f, 0f);
    }
    public void ResetWebSiteState()
    {
        currentPage = animator.GetInteger("page");
        currentPhase = 4;
        currentPopup = 0;
        currentHead = -1;
        checkListResult[0].transform.parent.gameObject.SetActive(true);
        ImageRecheck.overrideSprite = recheckSprite[0];
        ImageRecheck.SetAllDirty();
    }
    public void NextHead(int i)
    {
        if (i == currentHead)
        {
            return;
        }

        if (currentHead == -1)
        {
            currentHead = i;
            phase2_head.overrideSprite = Phase2_Head[currentHead];
            phase2_detail.overrideSprite = Phase2_DetailPopup[currentHead];
            phase2_Animator.SetTrigger("new");
            return;
        }

        currentHead = i;
        phase2_Animator.SetTrigger("endRe");
        StartCoroutine(NextHead());

        IEnumerator NextHead()
        {
            yield return new WaitForSeconds(4f);
            phase2_head.overrideSprite = Phase2_Head[currentHead];
            phase2_detail.overrideSprite = Phase2_DetailPopup[currentHead];
            phase2_Animator.SetTrigger("new");
        }
    }
    public void PlayHeadAudio(int index)
    {
        phase2_audio_head[(currentHead * (phase2_audio_head.Length / 3)) + index].Play();
    }
    public void CheckSum()
    {
        var score = 0f;
        for (var i = 0; i < checkListResult.Length; i++)
        {
            if (checkListResult[i].isOn)
            {
                switch (i)
                {
                    case 0:
                    case 2:
                    case 6:
                        score += 0.5f;
                        break;
                    case 3:
                    case 4:
                    case 5:
                        score += 2f;
                        break;
                    case 1:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                        score += 3f;
                        break;
                }
            }
        }

        if (score >= 6f)
        {
            phase3_videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "80%.mp4");
        }
        else if (score >= 2f)
        {
            phase3_videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "30%.mp4");
        }
        else if (score >= 0.1f)
        {
            phase3_videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "5%.mp4");
        }
        else
        {
            phase3_videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "0%.mp4");
        }

        arrowOnVideoPhase3.SetActive(true);

        phase3_videoPlayer.Prepare();
        phase3_videoPlayer.prepareCompleted += (vp) =>
        {
            vp.targetMaterialRenderer.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            vp.Play();
        };
    }
}