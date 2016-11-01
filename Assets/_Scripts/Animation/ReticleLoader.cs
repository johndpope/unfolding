using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;

public class ReticleLoader : MonoBehaviour
{
	public UnityEvent ShowVisualisations;
	public Image ReticleLoaderElement;
	public Image ReticleElement;
	public Image Instructions;
	public GameObject Delay;
	public RectTransform Toolbar;
	

	public void StartLoading()
	{
		ReticleLoaderElement.DOFillAmount(1f, 0.7f).SetEase(Ease.OutSine).OnComplete(PopUI);
	}

	void PopUI()
	{
		Delay.transform.DOMove(new Vector3(0f, 0f, 0f), 0.2f).OnComplete(TurnOnVisualisations);
		ReticleLoaderElement.transform.DOScale(1.1f, 0.3f).SetEase(Ease.OutBounce).OnComplete(HideLoader);
		ReticleLoaderElement.DOFade(0f, 0.3f).SetDelay(0.12f);
		ReticleElement.transform.DOScale(1.1f, 0.3f).SetEase(Ease.OutBounce).OnComplete(HideLoader);
		ReticleElement.DOFade(0f, 0.3f).SetDelay(0.12f);
		Instructions.DOFade(0f, 0.3f).SetDelay(0.12f);
		Toolbar.DOAnchorPosY(0f, 0.3f).SetEase(Ease.InSine);
	}

	void HideLoader()
	{
		ReticleLoaderElement.DOFade(0f, 0.05f);
	}

	void TurnOnVisualisations()
	{
		ShowVisualisations.Invoke();
		Delay.transform.position = new Vector3(2f, 0f, 0f);
	}

	public void ShowLoadUI()
	{
		ReticleLoaderElement.fillAmount = 0f;
		ReticleElement.DOFade(1f, 0.5f);
		ReticleLoaderElement.DOFade(1f, 0.5f);
		Instructions.DOFade(1f, 0.5f);
	}

	public void HideToolbar()
	{
		Toolbar.DOAnchorPosY(-90f, 0.2f).SetEase(Ease.OutSine);
	}
}
