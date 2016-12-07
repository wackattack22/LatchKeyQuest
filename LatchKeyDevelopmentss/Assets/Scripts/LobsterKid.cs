﻿using UnityEngine;
using System.Collections;

public class LobsterKid : MonoBehaviour
{

	private static GameObject[] LavaList;
	public GameObject shooter;

	GameObject player;

	public float time;
	private int lavaTile;
	public bool isVisible = false;
	public bool isKillable;

	private float lineOfSight;

	private Animator lobsterAnim;

	private BoxCollider2D boxCol;



	// Use this for initialization
	void Start()
	{
		lobsterAnim = GetComponent<Animator> ();
		boxCol = GetComponent<BoxCollider2D> ();
		//Load enemy projectile
		shooter = (GameObject)Resources.Load("Shooter");

		//Default to invisible when level starts
		this.GetComponent<Renderer>().enabled = false;

		//List of all lava tiles where enemy can appear
		LavaList = GameObject.FindGameObjectsWithTag("Lava");

		//Interval at which enemies appear
		//Randomly between 4 and 6 seconds
		time = Random.Range(4.0f, 6.0f);

		//Index of LavaList to appear on
		lavaTile = Random.Range(0, LavaList.Length);

		player = GameObject.FindGameObjectWithTag("Player");

		lineOfSight = 20f;
	}

	void FixedUpdate()
	{
		//Timer
		time -= Time.deltaTime;

		if (time <= 0)
		{   
			AppearRandomly(time, lavaTile);
		}

	}

	//Death
	void OnTriggerEnter2D(Collider2D col)
	{
		if (isVisible) { 
			if (col.gameObject.name == "projectile")
			{
				Kill();
				PlayerController.lvlScore += 25;
			}
		}
	}


	public void Kill()
	{
		lobsterAnim.SetBool ("isDead", true);
	}

	void DisableCollision(){
		boxCol.enabled = false;
	}

	void DestroySelf(){
		Destroy(this.gameObject);
	}

	//Logic for timing and positioning
	private void AppearRandomly(float time, int lavaTile)
	{
		if (!isVisible)
		{
			transform.position = LavaList[lavaTile].transform.position;
			GetComponent<Renderer>().enabled = true;
			isVisible = true;
			isKillable = true;
			this.time = 3;      //how long enemy is visible for
			if (Vector2.Distance(transform.position, player.transform.position) < lineOfSight)
				Shoot();
		}
		else
		{
			GetComponent<Renderer>().enabled = false;
			isVisible = false;
			isKillable = false;
			//Get new random time and vector
			this.time = Random.Range(4.0f, 6.0f);
			this.lavaTile = Random.Range(0, LavaList.Length);
		}


	}

	void Shoot()
	{

		GameObject projectile = Instantiate(shooter) as GameObject;

		projectile.name = "shooter";

		projectile.transform.position = transform.position;

	}
}