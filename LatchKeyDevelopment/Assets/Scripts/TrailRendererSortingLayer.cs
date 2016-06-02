using UnityEngine;
using System.Collections;

public class TrailRendererSortingLayer : MonoBehaviour {

	public TrailRenderer trail;
	
	void Start () {
		trail = GetComponent<TrailRenderer> ();
		trail.sortingOrder = 3;
	}
}
