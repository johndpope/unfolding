using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using LitJson;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;

public class SelectFeature : MonoBehaviour
{
	public StandaloneInputModule StandaloneInputModule;
	public UnityEvent TooltipSelected;
	public UnityEvent Unselected;
	public ScrollSnapRect ScrollSnapRect;
	public PaginationController PaginationController;

	public GameObject GalleryContainer;
	public int NumberOfImagesInGallery;
	public string ImageFolderName;

	private int galleryImageCount;
	private string featureDescriptions;
	private JsonData features;
	public Text Title;
	public Text Subtitle;
	public Text Description;

	public Color HighlightedTooltipColor;
	public Color DefaultTooltipColor;
	public Color HighlightedTooltipArrowColor;
	public Color DefaultTooltipArrowColor;

	private GameObject CurrentFeature;
	private GameObject PreviousFeature;

	private int fingerID = -1;

	private Tween infinitePulseTween;
	public LayerMask ViewSwitchLayerMask;


	void Start()
	{
		
		//Preload descriptions JSON file.
		string featureDescriptions = Resources.Load<TextAsset>("JSON/FeatureInformation").text;
		features = JsonMapper.ToObject(featureDescriptions);
	}

	void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			//Raycast from mouse or finger input.
			RaycastHit hitInfo;
			Ray rayOrigin = Camera.main.ScreenPointToRay (Input.mousePosition);
			if(Physics.Raycast(rayOrigin, out hitInfo, 100.0f, ViewSwitchLayerMask.value))
			{
				if(hitInfo.transform.tag != "UI")
				{
					CurrentFeature = GameObject.FindGameObjectWithTag(hitInfo.transform.tag);
					if(PreviousFeature != CurrentFeature)
					{
					//Load data to description panels
					ParseData(hitInfo.transform.tag);

					//Highlight current tooltip, fade previous
					HighlightCurrentTooltip(CurrentFeature, PreviousFeature);

					//Invoke tooltip selected event
					TooltipSelected.Invoke();

					//Handle UI
					CreateGallery(ImageFolderName);
					PaginationController.DestroyPagination(galleryImageCount);
					ScrollSnapRect.CurrentPageNumber = 0;
					ScrollSnapRect.PageHasChanged.Invoke();
					}
					PreviousFeature = CurrentFeature;
				}
			}
		}
	}

	void CreateGallery(string folderName)
	{
		for(int i = GalleryContainer.transform.childCount - 1; i >= 0; i--)
		{
			DestroyImmediate(GalleryContainer.transform.GetChild(i).gameObject);
		}
			
		GameObject[] loadedImages = Resources.LoadAll<GameObject>("Images" + "/" + folderName);
		for(int i = 0; i < loadedImages.Length; i++)
		{
			GameObject galleryImage = (GameObject)Instantiate(loadedImages[i], GalleryContainer.transform.position, Quaternion.identity);
			galleryImage.transform.parent = GalleryContainer.transform;
		}
		galleryImageCount = loadedImages.Length;
		ScrollSnapRect.InitiateSnapping(galleryImageCount);
	}

	void ParseData(string hitTag)
	{
		Title.text = (string)features["features"][hitTag]["title"];
		Subtitle.text = (string)features["features"][hitTag]["subtitle"];
		Description.text = Resources.Load<TextAsset>("Descriptions" + "/" + (string)features["features"][hitTag]["description"]).text;
		NumberOfImagesInGallery = int.Parse((string)features["features"][hitTag]["imageCount"]);
		ImageFolderName = (string)features["features"][hitTag]["imageFolderName"];
	}

	public void HideCurrentTooltip()
	{
		CurrentFeature.GetComponent<Renderer>().material.DOColor(DefaultTooltipColor, 0.3f);
		CurrentFeature.transform.parent.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.DOColor(DefaultTooltipArrowColor, 0.2f);
		PreviousFeature = null;
	}

	void HighlightCurrentTooltip(GameObject currentTooltip, GameObject previousTooltip)
	{
		currentTooltip.GetComponent<Renderer>().material.DOColor(HighlightedTooltipColor, 0.3f);
		currentTooltip.transform.parent.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.DOColor(HighlightedTooltipArrowColor, 0.2f);
		if(previousTooltip != null)
		{
			previousTooltip.GetComponent<Renderer>().material.DOColor(DefaultTooltipColor, 0.3f);
			previousTooltip.transform.parent.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.DOColor(DefaultTooltipArrowColor, 0.2f);
		}

		TooltipPulse(currentTooltip, previousTooltip);
	}

	void TooltipPulse(GameObject currentTooltip, GameObject previousTooltip)
	{
		infinitePulseTween = currentTooltip.transform.DOScale(new Vector3(1.15f, 1.15f, 1.15f), 0.75f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutSine).SetAutoKill(false);
		infinitePulseTween.OnComplete(infinitePulseTween.Restart);

		if(previousTooltip != null)
		{
			if(previousTooltip != currentTooltip)
			{
				KillTween();
				infinitePulseTween = currentTooltip.transform.DOScale(new Vector3(1.15f, 1.15f, 1.15f), 0.75f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutSine).SetAutoKill(false);
				infinitePulseTween.OnComplete(infinitePulseTween.Restart);
			}
		}
	}

	void KillTween()
	{
		infinitePulseTween.OnComplete(null);
		infinitePulseTween.SetAutoKill(true);
	}
}