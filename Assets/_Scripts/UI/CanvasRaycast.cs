using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CanvasRaycast : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler
{
	public UnityEvent HideContentPanel;
	private bool draggingStarted = false;
	public bool TooltipSelected = false;
	private int tapCount = 0;

	public void OnPointerClick( PointerEventData data )
	{
		if(!draggingStarted)
		{
			RaycastHit hitInfo;
			Ray rayOrigin = Camera.main.ScreenPointToRay (data.position);
			if(Physics.Raycast(rayOrigin, out hitInfo, 100.0f))
			{
				Debug.Log(hitInfo.transform.tag);
			}
			else
			{
				HideContentPanel.Invoke();
			}
		}
	}

	public void OnBeginDrag( PointerEventData data )
	{
		draggingStarted = true;
	}

	public void OnEndDrag( PointerEventData data )
	{
		draggingStarted = false;
	}
}