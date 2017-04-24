using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour {

	public float health = 150f;

	void OnTriggerEnter2D(Collider2D collider){
		Laser missile = collider.gameObject.GetComponent<Laser>();
		if(missile){
			health -= missile.GetDamage ();
			missile.Hit ();

			if(health <= 0){
				Destroy (gameObject);
			}
		}

	}
}
