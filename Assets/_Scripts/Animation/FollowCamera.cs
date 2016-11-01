using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour
{
	public Transform CameraTransform;
	public Transform IconPivotTransform;
	private float speed = 0.1f;

	void LateUpdate()
	{
		Quaternion targetRotation;
		targetRotation=Quaternion.LookRotation(CameraTransform.position-gameObject.transform.position);
		transform.rotation=Quaternion.Slerp(transform.rotation,targetRotation,Time.time * speed);
		IconPivotTransform.rotation = transform.rotation;
	}
}
