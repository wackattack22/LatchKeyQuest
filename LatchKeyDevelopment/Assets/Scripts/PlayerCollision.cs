using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour {

	public PlayerController playCont;

	void Start(){
		playCont = GetComponent<PlayerController> ();
	}
		
	void OnTriggerStay2D (Collider2D col)
	{
		if (col.gameObject.layer == 11) {  //Rift      
            StartCoroutine(playCont.NextScene());
		}
        else if (col.gameObject.layer == 14) {
			if (!playCont.isRolling) {
				playCont.Kill ();
			}
		}
	}
}