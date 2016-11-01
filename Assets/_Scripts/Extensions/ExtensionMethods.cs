using UnityEngine;
using System.Collections;

public static class ExtensionMethods
{
	//fromSource = a0
	//toSource = a1
	//fromTarget = b0
	//toTarget = b1
	public static float Map (this float x, float in_min, float in_max, float out_min, float out_max)
	{
		//return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;

		return out_min + (out_max - out_min) * ((x - in_min) / (in_max - in_min));
	}
}