﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	public float speed = 2f;

	private float xmin;
	private float xmax;
	private bool movingLeft;

    // Use this for initialization
    void Start () {
		SpawnEnemies ();
    }

	void SpawnEnemies(){
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
		xmin = leftBoundary.x;
		xmax = rightBoundary.x;

		foreach(Transform child in transform){
			GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}

		movingLeft = true;
	}

	void OnDrawGizmos(){
		Gizmos.DrawWireCube (transform.position, new Vector3(width, height, 0));
	}

	// Update is called once per frame
	void Update () {

		if (movingLeft) {
			this.transform.position += Vector3.left * speed * Time.deltaTime;
		} else {
			this.transform.position += Vector3.right * speed * Time.deltaTime;
		}

		float rightEdgeOfFormation = transform.position.x + (0.5f*width);
		float leftEdgeOfFormation = transform.position.x - (0.5f*width);

		if(leftEdgeOfFormation < xmin){
			movingLeft = false;
		}else if(rightEdgeOfFormation > xmax){
			movingLeft = true;
		}

		if(AllMembersDead()){
			Debug.Log("Empty Formation");
			SpawnEnemies ();
		}
	}

	bool AllMembersDead(){
		foreach(Transform childPositionGameObject in transform){
			if (childPositionGameObject.childCount > 0) {
				return false;
			}
		}
		return true;
	}

}
