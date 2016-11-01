using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class YearViewController : MonoBehaviour
{
	public UnityEvent Year1925;
	public UnityEvent Year1885;
	public UnityEvent Year1978;

	void Start()
	{
		//Year1925.Invoke();
	}

	public void YearSwitch(int currentYear)
	{
		if(currentYear == 0)
		{
			Year1925.Invoke();
		}
		else if(currentYear == 1)
		{
			Year1885.Invoke();
		}
		else if(currentYear == 2)
		{
			Year1978.Invoke();
		}
	}
}
