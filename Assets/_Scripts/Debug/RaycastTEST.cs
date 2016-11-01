using UnityEngine;
using System.Collections;

public class RaycastTEST : MonoBehaviour {

	public Camera main;

	private Ray ray;


	void Update() {
		ray = main.ScreenPointToRay(Input.mousePosition);
		RaycastHit[] hits;
		hits = Physics.RaycastAll(ray);
		int i = 0;
		while (i < hits.Length) {
			RaycastHit hit = hits[0];
			Debug.Log (hit.collider.gameObject.name);
			i++;
		}
	}
}