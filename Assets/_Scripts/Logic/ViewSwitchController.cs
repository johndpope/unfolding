using UnityEngine;
using System.Collections;

public class ViewSwitchController : MonoBehaviour
{
	public GameObject Visualisations;
	public GameObject[] modelViews;
	public GameObject[] drawingViews;
	public GameObject View1885;
	public GameObject View1978;

	void Start()
	{
		modelViews = GameObject.FindGameObjectsWithTag("Model");
		drawingViews = GameObject.FindGameObjectsWithTag("Drawing");
		for(int i = 0; i < drawingViews.Length; i++)
		{
			drawingViews[i].SetActive(false);
		}
		View1885.SetActive(false);
		View1978.SetActive(false);
		Visualisations.SetActive(false);
	}

	public void ModelViewOn()
	{
		for(int i = 0; i < modelViews.Length; i++)
		{
			modelViews[i].SetActive(true);
		}
		for(int i = 0; i < drawingViews.Length; i++)
		{
			drawingViews[i].SetActive(false);
		}
	}
	public void DrawingViewOn()
	{
		for(int i = 0; i < modelViews.Length; i++)
		{
			modelViews[i].SetActive(false);
		}
		for(int i = 0; i < drawingViews.Length; i++)
		{
			drawingViews[i].SetActive(true);
		}
	}
}
