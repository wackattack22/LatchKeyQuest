﻿using UnityEngine;
using System.Collections;

public class BoblinController : MonoBehaviour {

	private Vector2 moveDir;
	private int moveChoice;

    private float time;

    private bool colliding;

	private Animator boblinAnim;

	private bool isWalking;

	public PlayerController playerController;

	private BoxCollider2D boxCol;


    void Start () {
        //Random direction

		boblinAnim = GetComponent<Animator>();
		moveChoice = Random.Range (-2, 2);
		playerController = GameObject.Find ("Player").GetComponent<PlayerController> ();
		boxCol = GetComponent<BoxCollider2D> ();

		switch (moveChoice) 
		{
		case -1:
			moveDir = Vector2.down;
			break;
		case 1:
			moveDir = Vector2.up;
			break;
		case 2:
			moveDir = Vector2.right;
			break;
		case -2:
			moveDir = Vector2.left;
			break;
		case 0:
			moveChoice = Random.Range (-2, 2);
			break;
		}


        //Random time to move in direction
        time = Random.Range(1f, 3f);

    }
	
	void Update () {
        time -= Time.deltaTime;

		if (boblinAnim.GetBool ("isDead") == false) {
			this.GetComponent<Rigidbody2D> ().velocity = moveDir.normalized * 2f;
		} else {
			this.GetComponent<Rigidbody2D> ().velocity = moveDir.normalized * 0f;
		}

		boblinAnim.SetInteger ("moveChoice", moveChoice);

		switch (moveChoice) 
		{
		case -1:
			moveDir = Vector2.down;
			break;
		case 1:
			moveDir = Vector2.up;
			break;
		case 2:
			moveDir = Vector2.right;
			break;
		case -2:
			moveDir = Vector2.left;
			break;
		case 0:
			moveChoice = Random.Range (-2, 2);
			break;
		}

      if (colliding)    //Reverse direction
        {   
			moveChoice *= -1;
			colliding = false;
        }
        
        //Time is up, new random direction and interval
        if (time <= 0)
        {
			moveChoice = Random.Range (-2, 2);
            
			switch (moveChoice) 
			{
			case -1:
				moveDir = Vector2.down;
				break;
			case 1:
				moveDir = Vector2.up;
				break;
			case 2:
				moveDir = Vector2.right;
				break;
			case -2:
				moveDir = Vector2.left;
				break;
			case 0:
				moveChoice = Random.Range (-2, 2);
				break;
			}

            time = Random.Range(1f, 3f);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //collisions that reverse direction
        if (col.gameObject.layer == 8)  //wall
        {
            colliding = true;
        }
        else if (col.gameObject.layer == 9) //hazard
        {
            colliding = true;
        }
		else if (col.gameObject.tag == "Lava")
		{
			colliding = true;
		}
        else if (col.gameObject.layer == 13)    //enemy
        {
            colliding = true;
        }
        else if (col.gameObject.tag == "Switch")
        {
            colliding = true;
		}
		else if(col.gameObject.layer == 12){ // player
			playerController.Kill();
		}
		else if (col.gameObject.layer == 15)    //blocker
		{
			colliding = true;
		}

        //collisions that kill boblin
        if (col.gameObject.name == "projectile")
        {
            PlayerController.lvlScore += 10;
            Kill();
        }
    }

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.layer == 9) { //hazard
			colliding = true; 
		} else if (col.gameObject.layer == 15) { //blocker
			colliding = true; 
		} else if (col.gameObject.layer == 12) {
			colliding = true;
		}
	}

    public void Kill()
    {
		boblinAnim.SetBool ("isDead", true);
    }

	void DisableCollision(){
		boxCol.enabled = false;
	}

	void DestroySelf(){
		Destroy(this.gameObject);
	}
}
