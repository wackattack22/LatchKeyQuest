using UnityEngine;
using System.Collections;

public class HazardCollision : MonoBehaviour {

	public PlayerController playerController;

	// Use this for initialization
	void Start () {
		playerController = GameObject.Find ("Player").GetComponent<PlayerController> ();
	}
	

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.layer == 12){ // player
			playerController.Kill();
		}
        //Blocking logic for projectile hazards
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.layer == 12){ // player
			playerController.Kill();
		}
	}
}
