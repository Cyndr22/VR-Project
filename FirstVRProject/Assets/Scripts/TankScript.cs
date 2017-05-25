using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankScript : MonoBehaviour {

	int tankHealth = 100;
	int damageAmmount = 10;

	public Transform target;
	NavMeshAgent agent;

	private PlayerController pc;
	private bool attacking = true;
	private float attackRange = 15f;
	private float attackRate = 5f;
	private float timer;
	void DeductPoints (int damageAmount)
	{
		tankHealth -= damageAmount;
	}

	// Use this for initialization
	void Start () {
		GameObject p = GameObject.FindGameObjectWithTag ("Player");
		if (p != null) {
			pc = p.GetComponent <PlayerController> ();
		}
		agent = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (pc == null) {
			GameObject p = GameObject.FindGameObjectWithTag ("Player");
			if (p != null) {
				pc = p.GetComponent <PlayerController> ();
			}
		}
		if (attacking) {
			if (Vector3.Distance (transform.position, target.position) < attackRange) {
				timer += Time.deltaTime;
				Attack ();
			}
		}

		if (Vector3.Distance (transform.position, target.position) < 15f) {
			RaycastHit hit;
			Vector3 direction = target.position - transform.position;
			if (Physics.Raycast(transform.position, direction, out hit, 50))
			{
				agent.SetDestination (target.position);
			}
		}
		if(tankHealth <= 0)
		{
			Destroy(gameObject);
		}
	}

		void Attack () {

		if (timer >= attackRate) {
			timer = 0;

			Debug.Log ("EETWARKS");
			if (pc != null) {
				if (pc.getHealth () > 0) {
					pc.playerDamage (25);
				}
			} else {
				Debug.Log ("Player Object not Found");
			}
		}
	}
}
