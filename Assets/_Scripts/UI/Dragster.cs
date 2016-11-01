using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Linq;
using UnityEngine.Events;

public class Dragster : MonoBehaviour,
IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
	public UnityEvent MiddleStateDocked;
	public UnityEvent MiddleStateMovingTowardsFooter;
	public SelectFeature FeatureSelector;

	//public GameObject ARCam;
	public GameObject Center;
	public GameObject FooterAnchor;
	public GameObject MiddleAnchor;
	public GameObject HeaderAnchor;
	public GameObject[] Anchors;
	public GameObject Gallery;
	public GameObject GalleryAnchor;
	public GameObject BaseAnchor;
	public GameObject ViewSwitchPanel;

	public GameObject ActionButton;
	public GameObject BackButton;
	public GameObject GalleryGradientOverlay;
	public CanvasGroup LogoCanvasGroup;

	public GameObject FooterBlock;
	public GameObject HeaderBlock;

	private float footerAnchorY;
	private float distance;
	private Vector2 clickPosition;
	private float nextAnchorProximity;
	public CanvasGroup GalleryCanvasGroup;

	public Text PanelTitleText;
	public Text PanelDescriptionText;
	public Image PanelBG;

	public ScrollRect PanelScrollRect;

	private Vector2 StartPosition;
	private Vector2 GalleryDestinationPosition;
	private Vector2 GalleryStartPosition;
	private Vector2 BasePosition;

	private HSBColor footerTextColor = new HSBColor(0f,0f,0.13f);
	private HSBColor activeTextColor = new HSBColor(0f,0f,0.96f);
	private HSBColor footerBGColor = new HSBColor(0f,0f,0.88f);
	private HSBColor activeBGColor = new HSBColor(0f,0f,0.26f);

	bool headerState = false;
	bool middleState = false;
	bool footerState = false;

	private bool middleStateAlreadyDocked = false;

	void Start()
	{
		SetColors();
		BasePosition = new Vector2(Center.transform.position.x, BaseAnchor.transform.position.y);
		StartPosition = new Vector2(Center.transform.position.x, FooterBlock.transform.position.y);
		GalleryStartPosition = new Vector2(Gallery.transform.position.x, Gallery.transform.position.y);
		GalleryDestinationPosition = new Vector2(GalleryAnchor.transform.position.x, GalleryAnchor.transform.position.y);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		distance = eventData.position.y - Center.transform.position.y;
	}

	public void OnBeginDrag(PointerEventData eventData) {}

	public void OnDrag(PointerEventData eventData)
	{
		clickPosition = new Vector2(Center.transform.position.x, eventData.position.y);
		float draggableObjectCenterY = clickPosition.y - distance;
		Center.transform.position = new Vector2(Center.transform.position.x, Mathf.Clamp(draggableObjectCenterY, FooterBlock.transform.position.y, HeaderBlock.transform.position.y));

		if(FindClosestTarget().tag == "HeaderAnchor")
		{
			//Debug.Log("Header state");
			headerState = true;
			middleState = false;
			footerState = false;
		}
		else if(FindClosestTarget().tag == "MiddleAnchor")
		{
			//Debug.Log("Middle state");
			headerState = false;
			middleState = true;
			footerState = false;
		}
		else if(FindClosestTarget().tag == "FooterAnchor")
		{
			//Debug.Log("Footer state");
			headerState = false;
			middleState = false;
			footerState = true;
		}
	}

	void Update()
	{
		float normalisedObjectPositionY = Center.transform.position.y / Screen.height;

		if(headerState)
		{
			//float nextAnchorProximity = Mathf.Clamp(normalisedObjectPositionY.Map(0.72f, 0.95f, 1f, 0f), 0f, 1f);
			HideActionButtonOnHeader(normalisedObjectPositionY);
			DisableScrollRect();
			ResetColorsOnHeader();
			DisableViewSwitcher();
		}
		else if(middleState)
		{
			//float nextAnchorProximity = Mathf.Clamp(normalisedObjectPositionY.Map(0.27f, 0.72f, 0f, 1f), 0f, 1f);
			FooterToMiddleTransition(normalisedObjectPositionY);
			DisableScrollRect();
			ResetColorsOnMiddle();
			DisableViewSwitcher();
		}
		else if(footerState)
		{
			//float nextAnchorProximity = Mathf.Clamp(normalisedObjectPositionY.Map(0.04f, 0.27f, 0f, 1f), 0f, 1f);
			DisableScrollRect();
			ResetColorsOnFooter();
			ResetGalleryPosition();
			EnableViewSwitchPanel();
		}
	}
		
	public void OnEndDrag(PointerEventData eventData)
	{
		float nearestAchorPositionY = FindClosestTarget().GetComponent<RectTransform>().position.y;
		Center.transform.DOMoveY(nearestAchorPositionY, 0.5f, false);
	}

	void actionButtonFader(float proximity)
	{
		ActionButton.GetComponent<CanvasGroup>().alpha = proximity;
	}

	GameObject FindClosestTarget()
	{
		Vector3 position = new Vector3(Center.transform.position.x, Center.transform.position.y, 0);
		return Anchors
			.OrderBy(o => (o.transform.position - position).sqrMagnitude)
			.FirstOrDefault();
	}

	void SetColors()
	{
		PanelTitleText.color = footerTextColor.ToColor();
		PanelDescriptionText.color = footerTextColor.ToColor();
		PanelBG.color = footerBGColor.ToColor();
	}

	void ResetColorsOnFooter()
	{
		ActionButton.GetComponent<CanvasGroup>().alpha = 1f;
		BackButton.GetComponent<CanvasGroup>().alpha = 0f;
		GalleryGradientOverlay.GetComponent<CanvasGroup>().alpha = 0f;
		PanelBG.color = footerBGColor.ToColor();
		PanelTitleText.color = footerTextColor.ToColor();
		PanelDescriptionText.color = footerTextColor.ToColor();
	}

	void ResetColorsOnMiddle()
	{
		ActionButton.GetComponent<CanvasGroup>().alpha = 1f;
	}

	void ResetColorsOnHeader()
	{
		BackButton.GetComponent<CanvasGroup>().alpha = 1f;
		GalleryGradientOverlay.GetComponent<CanvasGroup>().alpha = 1f;
		PanelBG.color = activeBGColor.ToColor();
		PanelTitleText.color = activeTextColor.ToColor();
		PanelDescriptionText.color = activeTextColor.ToColor();
	}

	void FooterToMiddleTransition(float normPosY)
	{
		float middleStatePosition = Mathf.Clamp(normPosY.Map(0.27f, 0.495f, 0f, 1f), 0f, 1f);
		float panelBGBrightness = 1f - Mathf.Clamp(normPosY.Map(0.27f, 0.5f, 0.22f, 0.74f), 0.22f, 0.74f);
		float textBrightness = Mathf.Clamp(normPosY.Map(0.27f, 0.5f, 0.13f, 0.96f), 0.13f, 0.96f);
		float headerAlpha = Mathf.Clamp(normPosY.Map(0.27f, 0.5f, 0f, 1f), 0f, 1f);
		float logoAlpha = Mathf.Clamp(normPosY.Map(0.27f, 0.4f, 1f, 0f), 0f, 1f);
		float galleryPositionY = Mathf.Clamp(normPosY.Map(0.27f, 0.495f, 0f, 1f), 0f, 1f);

		PanelBG.color = new HSBColor(0f,0f,panelBGBrightness).ToColor();
		PanelTitleText.color = new HSBColor(0f,0f,textBrightness).ToColor();
		PanelDescriptionText.color = new HSBColor(0f,0f,textBrightness).ToColor();

		LogoCanvasGroup.alpha = logoAlpha;
		BackButton.GetComponent<CanvasGroup>().alpha = headerAlpha;
		GalleryGradientOverlay.GetComponent<CanvasGroup>().alpha = headerAlpha;
		Gallery.transform.position = new Vector2(Gallery.transform.position.x, GalleryDestinationPosition.y * galleryPositionY);

		if(middleState)
		{
			if(middleStatePosition == 1f)
			{
				MiddleStateDocked.Invoke();
				middleState = false;
				middleStateAlreadyDocked = true;
			}
			if(middleStateAlreadyDocked)
			{
				if(middleStatePosition < 1f)
				{
					MiddleStateMovingTowardsFooter.Invoke();
					middleState = false;
					middleStateAlreadyDocked = false;
				}
			}
		}
	}

	void HideActionButtonOnHeader(float normPosY)
	{
		float nextAnchorProximity = Mathf.Clamp(normPosY.Map(0.72f, 0.95f, 1f, 0f), 0f, 1f);
		ActionButton.GetComponent<CanvasGroup>().alpha = nextAnchorProximity;
		BackButton.GetComponent<CanvasGroup>().alpha = 1f;
	}

	void EnableScrollRect()
	{
		PanelScrollRect.enabled = true;
	}

	void DisableScrollRect()
	{
		PanelScrollRect.enabled = false;
		PanelScrollRect.verticalNormalizedPosition = 1f;
	}

	public void ResetPositionOnBackArrow()
	{
		Center.transform.DOMove(StartPosition, 0.2f, false);
		headerState = false;
		middleState = false;
		footerState = true;
		ResetColorsOnFooter();
		Gallery.transform.position = GalleryStartPosition;
	}

	void ResetGalleryPosition()
	{
		Gallery.transform.position = GalleryStartPosition;
	}

	public void ResetPanelPosition()
	{
		Center.transform.DOMove(BasePosition, 0.2f, false);
		GalleryCanvasGroup.alpha = 0;
		FeatureSelector.HideCurrentTooltip();
		ResetColorsOnFooter();
	}

	void DisableViewSwitcher()
	{
		ViewSwitchPanel.SetActive(false);
	}

	void EnableViewSwitchPanel()
	{
		ViewSwitchPanel.SetActive(true);
	}
}