using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class ToggleBehaviourHandler : MonoBehaviour
{
	public Toggle ToggleButton;

	public UnityEvent ToggleIsOn;
	public UnityEvent ToggleIsOff;

	void Start()
	{
		if(ToggleButton.isOn)
		{
			ToggleIsOn.Invoke();
		}
	}

	public void ToggleHandler()
	{
		if(ToggleButton.isOn)
		{
			ToggleIsOn.Invoke();
		}
		else if(!ToggleButton.isOn)
		{
			ToggleIsOff.Invoke();
		}
	}
}
