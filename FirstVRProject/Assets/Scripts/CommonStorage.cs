using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonStorage : MonoBehaviour {

	// Use this for initialization
	void Start () {
		print ("hi");
		if (this.CompareTag ("Player")) {
			print (GetComponent<PlayerController> ().ToString ());
		} else {
			print (GetComponent<EnemyScript> ().ToString ());
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
