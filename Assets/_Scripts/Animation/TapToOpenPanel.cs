using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using DG.Tweening;

public class TapToOpenPanel : MonoBehaviour, IPointerClickHandler
{
	public Transform PanelAnchor;
	public Transform MiddleBlock;

	public void OnPointerClick(PointerEventData eventData)
	{
		PanelAnchor.DOMoveY(MiddleBlock.transform.position.y, 0.5f);
	}
}