using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour {

	int enemyHealth = 20;
	int damageAmmount = 10;
	int otherHealth;

	Animator anim;
	public Transform target;
	NavMeshAgent agent;

	private PlayerController pc;
	private bool attacking = true;
	private float attackRange = .5f;
	private float attackRate = 1f;
	private float timer;
	void DeductPoints (int damageAmount)
	{
		enemyHealth -= damageAmount;
	}

	void Start ()
	{
		GameObject p = GameObject.FindGameObjectWithTag ("Player");
		if (p != null) {
			pc = p.GetComponent <PlayerController> ();
		}
		anim = GetComponent<Animator> ();
		agent = GetComponent<NavMeshAgent> ();
	}

	void Update ()
	{
		
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
		if(enemyHealth <= 0)
		{
			Destroy(gameObject);
		}
	}

	void Attack () {
		
		if (timer >= attackRate)
		{
			timer = 0;

			Debug.Log ("EETWARKS");
				if (pc != null) {
					if (pc.getHealth () > 0) {
						pc.playerDamage (10);
					}
				} else {
					Debug.Log ("Player Object not Found");
				}
				//otherHealth = other.SendMessage ("getHealth", SendMessageOptions.DontRequireReceiver);
			}
		}
		
}
