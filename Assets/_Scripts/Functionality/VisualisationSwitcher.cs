using UnityEngine;
using System.Collections;

public class VisualisationSwitcher : MonoBehaviour
{
	public GameObject Visualisation;

	void OnEnable()
	{
		Visualisation.SetActive(true);
	}

	void OnDisable()
	{
		Visualisation.SetActive(false);
	}
}
