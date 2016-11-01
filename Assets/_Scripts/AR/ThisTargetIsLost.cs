using UnityEngine;
using System.Collections;

public class ThisTargetIsLost : MonoBehaviour {

	public bool isLost;

	public void TargetIsLost()
	{
		isLost = true;
	}

	public void TargetIsFound()
	{
		isLost = false;
	}
}
