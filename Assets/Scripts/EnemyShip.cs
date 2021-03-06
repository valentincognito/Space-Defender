﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour {

	public float health = 150f;
	public float laserSpeed = 5f;
	public GameObject laserPrefab;
	public float shotsPerSeconds = 0.5f;
	public int scoreValue = 150;
	public AudioClip explosion;
	public AudioClip fireSound;

	private ScoreKeeper scoreKeeper;

	void Start(){
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper>();
	}

	void Update(){
		float probablity = shotsPerSeconds * Time.deltaTime;
		if(Random.value < probablity){
			Fire ();
		}
	}

	void Fire(){
		Vector3 startPosition = this.transform.position + new Vector3(0, -1, 0);
		GameObject laser = Instantiate(laserPrefab, startPosition, Quaternion.identity) as GameObject;
		laser.GetComponent<Rigidbody2D>().velocity = new Vector3 (0, -laserSpeed, 0);

		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}

	void OnTriggerEnter2D(Collider2D collider){
		Laser missile = collider.gameObject.GetComponent<Laser>();
		if(missile){
			health -= missile.GetDamage ();
			missile.Hit ();

			if(health <= 0){
				AudioSource.PlayClipAtPoint(explosion, transform.position);
				Destroy (gameObject);
				scoreKeeper.Score (scoreValue);
			}
		}

	}

}
