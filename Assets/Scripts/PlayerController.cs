﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 15;
    public float padding = 1f;
	public float laserSpeed;
	public float firingRate = 0.2f;
	public float health= 250f;
	public GameObject laserPrefab;
	public AudioClip fireSound;

    float xmin;
    float xmax;

	// Use this for initialization
	void Start () {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xmin = leftmost.x + padding;
        xmax = rightmost.x - padding;
    }

	void Update () {
        if (Input.GetKey(KeyCode.LeftArrow)){
            this.transform.position += Vector3.left * speed * Time.deltaTime;
        }else if (Input.GetKey(KeyCode.RightArrow)){
            this.transform.position += Vector3.right * speed * Time.deltaTime;
		}

		if (Input.GetKeyDown(KeyCode.Space)){
			InvokeRepeating ("Fire", 0.000001f, firingRate);
		}
		if(Input.GetKeyUp(KeyCode.Space)){
			CancelInvoke ("Fire");
		}


        // restrict the player to the game space
        float newX = Mathf.Clamp(this.transform.position.x, xmin, xmax);
        this.transform.position = new Vector3(newX, this.transform.position.y, this.transform.position.z);
    }

	void Fire(){
		Vector3 offset = new Vector3 (0, 1, 0);
		GameObject laser = Instantiate(laserPrefab, transform.position + offset, Quaternion.identity) as GameObject;
		laser.GetComponent<Rigidbody2D>().velocity = new Vector3 (0, laserSpeed, 0);

		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}


	void OnTriggerEnter2D(Collider2D collider){
		Laser missile = collider.gameObject.GetComponent<Laser>();
		if(missile){
			health -= missile.GetDamage ();
			missile.Hit ();

			if(health <= 0){
				LevelManager man = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
				man.LoadLevel ("Win Screen");
				Destroy (gameObject);
			}
		}

	}
}
