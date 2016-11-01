using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class TargetOnScreenPosition : MonoBehaviour
{
	const float TARGET_RANGE_MIN = -1f;
	const float TARGET_RANGE_MAX = 1f;

	public UnityEvent TargetIsTooFar;
	public UnityEvent TargetIsSafe;

	public GameObject Target;
	public Camera ARcam;
	public float TargetVisibility = 0f;

	public GameObject RedFrame;
	public float TargetWarningLimit;

	bool targetIsSafe;

	void Update()
	{
		Vector3 targetScreenPos = ARcam.WorldToScreenPoint(Target.transform.position);

		float normalisedTargetScreenX = Mathf.Abs(map(targetScreenPos.x, 0f, Screen.width, TARGET_RANGE_MIN, TARGET_RANGE_MAX));
		float normalisedTargetScreenY = Mathf.Abs(map(targetScreenPos.y, 0f, Screen.height, TARGET_RANGE_MIN, TARGET_RANGE_MAX));
		TargetVisibility = Mathf.Max(normalisedTargetScreenX, normalisedTargetScreenY);

		if(targetIsSafe)
		{
			if(TargetVisibility > TargetWarningLimit)
			{
				TargetIsTooFar.Invoke();
				targetIsSafe = false;
			}
		}
		if(!targetIsSafe)
		{
			if (TargetVisibility < TargetWarningLimit)
			{
				TargetIsSafe.Invoke();
				targetIsSafe = true;
			}
		}
	}

	float map(float s, float a1, float a2, float b1, float b2)
	{
		return b1 + (s-a1)*(b2-b1)/(a2-a1);
	}
}
