using UnityEngine;
using System.Collections;

public class BlockerScript : MonoBehaviour {

	public AudioSource source;

	public AudioClip blockedAttackSound;
	

	void Awake(){
		source = GetComponent<AudioSource> ();
	}

	
	void OnTriggerStay2D (Collider2D col)
	{
		Physics2D.IgnoreCollision (GetComponent<BoxCollider2D> (), col);
	}
		
	
	void OnCollisionEnter2D (Collision2D col)
	{
		
		if (col.gameObject.layer == 14) {    //Projectile
			source.PlayOneShot(blockedAttackSound, 0.5f);
		}
	}

	void OnTriggerEnter2D (Collider2D col)
	{

		if (col.gameObject.layer == 14) {    //Projectile
			source.PlayOneShot(blockedAttackSound, 0.5f);
		}
	}
}
