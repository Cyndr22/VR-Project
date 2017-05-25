using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public int maxHealth;
	public int currentHealth;

	private GameController gameController;

	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController> ();
		}
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}
		currentHealth = maxHealth;
	}

	// Update is called once per frame
	void Update () {
		if (gameController != null) {
			if (currentHealth <= 0) {
				Debug.Log ("Dead");
				currentHealth = 100;
				transform.position = Vector3.zero;
			}
		}
		if (transform.position.y < -50) {
			transform.position = Vector3.zero;
			currentHealth = 100; 
		}
	}

	public void playerDamage (int damageRecieved) {
		currentHealth -= damageRecieved;
		Debug.Log (currentHealth);
	}

	public int getHealth (){
		return currentHealth;
	}

	public void OnCollisionEnter(Collision collision){
		if (collision.gameObject.CompareTag("Hazard"))
		{
			if (currentHealth > 0) {
				playerDamage (20);
			}
		}
	}
}
