using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;

    // Use this for initialization
    void Start () {
        
		foreach(Transform child in transform){
			GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
    }

	void OnDrawGizmos(){
		Gizmos.DrawWireCube (transform.position, new Vector3(width, height, 0));
	}

	// Update is called once per frame
	void Update () {
		
	}
}
