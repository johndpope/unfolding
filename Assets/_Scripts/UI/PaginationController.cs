using UnityEngine;
using System.Collections.Generic;

public class PaginationController : MonoBehaviour
{
	public SelectFeature SelectFeature;
	public GameObject PaginationContainer;
	public GameObject GalleryAnchor;
	public GameObject GalleryContainer;
	public GameObject ScrollSnapRect;
	public List<GameObject> GalleryContent;
	public List<GameObject> PaginationIndicatorDots;

	const int gutter = 26;
	private int previousPageNumber;
	private int galleryPageCount;
	private GameObject paginationIndicator;

	void Start()
	{
		paginationIndicator = (GameObject)Resources.Load("GUI/PaginationIndicator");
	}

	public void CreatePagination(int pageCount)
	{
		galleryPageCount = pageCount;
		if(galleryPageCount > 1)
		{
			for(int i = 0; i < galleryPageCount; i++)
			{
				GameObject paginationDot = (GameObject)Instantiate(paginationIndicator,
																   new Vector3((PaginationContainer.transform.position.x
																   - (((galleryPageCount - 1) * gutter)) / 2) + i * gutter,
																   PaginationContainer.transform.position.y, 0f),
																   Quaternion.identity); 
				paginationDot.transform.parent = PaginationContainer.transform;
				paginationDot.GetComponent<RectTransform>().localScale = new Vector3(0.7f, 0.7f, 0f);
				PaginationIndicatorDots.Insert(i, paginationDot);
			}
		}
	}

	public void DestroyPagination(int pageCount)
	{
		//Clear pagination
		PaginationIndicatorDots.Clear();
		for(var i = PaginationIndicatorDots.Count - 1; i > -1; i--)
		{
			PaginationIndicatorDots.RemoveAt(i);
		}
		foreach (Transform child in PaginationContainer.transform)
		{
			Destroy(child.gameObject);
		}
	
		//Create pagination
		CreatePagination(pageCount);
	}

	int GalleryPagesCount()
	{
		foreach (Transform child in GalleryContainer.transform)
		{
			GalleryContent.Add(child.gameObject);
		}
		return GalleryContent.Count;
	}

	public void HighlighCurrentPageIndicator()
	{
		if(galleryPageCount > 1)
		{
			int currentPageNumber = ScrollSnapRect.GetComponent<ScrollSnapRect>().CurrentPageNumber;

			if(previousPageNumber != currentPageNumber)
			{
				if(previousPageNumber < galleryPageCount)
				{
					PaginationIndicatorDots[previousPageNumber].gameObject.GetComponent<RectTransform>().localScale = new Vector3(0.7f, 0.7f, 0f);
				}
			}

			PaginationIndicatorDots[currentPageNumber].gameObject.GetComponent<RectTransform>().localScale = new Vector3(0.9f, 0.9f, 0f);

			previousPageNumber = ScrollSnapRect.GetComponent<ScrollSnapRect>().CurrentPageNumber;
		}
	}
}