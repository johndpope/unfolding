using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class SlideContentPanel : MonoBehaviour
{
	public CanvasGroup GalleryCanvasGroup;
	public GameObject FooterAnchor;
	public GameObject GalleryPanel;
	public GameObject ContentPanel;

	private float contentPanelInitialPositionY;

	void Start()
	{
		contentPanelInitialPositionY = ContentPanel.transform.position.y;
	}

	public void ShowPanel()
	{
		ContentPanel.transform.DOMoveY(FooterAnchor.transform.position.y, 0.2f, false).OnComplete(ShowGallery);
	}

	public void HidePanel()
	{
		ContentPanel.transform.DOMoveY(contentPanelInitialPositionY, 0.2f, false).OnStart(HideGallery);
	}
		
	void ShowGallery()
	{
		GalleryCanvasGroup.alpha = 1;
	}

	void HideGallery()
	{
		GalleryCanvasGroup.alpha = 0;
	}
}
