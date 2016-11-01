using UnityEngine;
using System.Collections;

public class Switch3DGUI : MonoBehaviour
{
	public GameObject[] GUIComponents;

	public void HideTooltips()
	{
		for(int i = 0; i < GUIComponents.Length; i++)
		{
			GUIComponents[i].SetActive(false);
		}
	}

	public void ShowTooltips()
	{
		for(int i = 0; i < GUIComponents.Length; i++)
		{
			GUIComponents[i].SetActive(true);
		}
	}
}