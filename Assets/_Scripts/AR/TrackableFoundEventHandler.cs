using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;


namespace Vuforia
{
	public class TrackableFoundEventHandler : MonoBehaviour,
	ITrackableEventHandler
	{

		public UnityEvent TrackingFound;
		public UnityEvent TrackingLost;
		public UnityEvent FirstMarkerFound;
		public TargetStateController TargetStateController;

		private TrackableBehaviour mTrackableBehaviour;
		private Vector3 originalTargetPosition;
		private Quaternion originalTargetRotation;

		void Start()
		{
			originalTargetPosition = gameObject.transform.position;
			originalTargetRotation = gameObject.transform.rotation;
			mTrackableBehaviour = GetComponent<TrackableBehaviour>();
			if(mTrackableBehaviour)
			{
				mTrackableBehaviour.RegisterTrackableEventHandler(this);
			}
		}

		public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus,
			TrackableBehaviour.Status newStatus)
		{
			if(newStatus == TrackableBehaviour.Status.DETECTED ||
				newStatus == TrackableBehaviour.Status.TRACKED ||
				newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
			{
				OnTrackingFound();
			}
			else
			{
				OnTrackingLost();
			}
		}


		private void OnTrackingFound()
		{
			//Do something when marker is found
			if(TargetStateController.AreAllTargetsLost)
			{
				FirstMarkerFound.Invoke();
				//TrackingFound.Invoke();
			}
			else
			{
				TrackingFound.Invoke();
			}
		}

		private void OnTrackingLost()
		{
			//Do something when marker is lost
			TrackingLost.Invoke();
			gameObject.transform.position = originalTargetPosition;
			gameObject.transform.rotation = originalTargetRotation;
		}
	}
}