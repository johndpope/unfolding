using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Linq;

public class TargetStateController : MonoBehaviour
{
	public UnityEvent VisualisationOff;

	public List<bool> AllTargetStates;
	public GameObject[] FrameTargets;

	private Vector3[] targetPositions;
	private Quaternion[] targetRotations;

	public bool AreAllTargetsLost = false;
	private bool checkForTargets = false;

	void Update ()
	{
		for(int i = 0; i < FrameTargets.Length; i++)
		{
			AllTargetStates[i] = FrameTargets[i].GetComponent<ThisTargetIsLost>().isLost;
		}
		AreAllTargetsLost = AllTargetStates.All(a => a) || AllTargetStates.All(a => !a);

		if(!checkForTargets)
		{
			if(!AreAllTargetsLost)
			{
				checkForTargets = true;
			}
		}

		if(checkForTargets)
		{
			if(AreAllTargetsLost)
			{
				VisualisationOff.Invoke();
				checkForTargets = false;
			}
		}
	}
}
