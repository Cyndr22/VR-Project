using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VRTK
{
	public class VRGun : MonoBehaviour {

		public GameObject[] gun;
		public GameObject impact;
		private GameObject bullet;
		private Transform fp;
		private int currentGun = 0;
		private float bulletSpeed = 500f;
		private float bulletLife = 1f;

		private float[] damageAmount = {0f,2f,.2f,1f,10f};
		private bool autoFire;
		private double timer;
		private double rifleRate = .2f;
		private double smgRate = .1f;

		public void swapGun(int num)
		{
			gun [currentGun].SetActive (false);
			gun [num].SetActive (true);
			currentGun = num;
		}

	protected void Start()
	{
		Transform bul = gun[4].transform.Find ("Bullet");
		fp = gun[1].transform.Find ("Bullet");
		if(bul != null)
		{
			bullet = bul.gameObject;
			bullet.SetActive(false);
		}
		GetComponent<VRTK_ControllerEvents>().TriggerPressed += new ControllerInteractionEventHandler(FireBulletEvent);
			GetComponent<VRTK_ControllerEvents>().TriggerReleased += new ControllerInteractionEventHandler(StopAutoFire);
		
	
	}
			
		private void StopAutoFire(object sender, ControllerInteractionEventArgs arg)
		{
			autoFire = false;
		}


	private void FireBulletEvent(object sender, ControllerInteractionEventArgs arg)
		{
			if (currentGun == 1 || currentGun == 3) {
				autoFire = true;
				timer = 0f;
			}
			if (currentGun == 4) {
				GameObject bulletClone = Instantiate (bullet, bullet.transform.position, bullet.transform.rotation) as GameObject;
				bulletClone.SetActive (true);
				Rigidbody rb = bulletClone.GetComponent<Rigidbody> ();
				rb.AddForce (bullet.transform.up * bulletSpeed);
				Destroy (bulletClone, bulletLife);
			} else if (currentGun == 1 || currentGun == 3) {
				RaycastHit shot;
				if (Physics.Raycast (fp.position, fp.forward, out shot, 30.0f)) {
					GameObject clone = Instantiate (impact);
					clone.transform.position = shot.point;

					Destroy (clone, 1f);
					shot.transform.SendMessage ("DeductPoints", damageAmount [currentGun], SendMessageOptions.DontRequireReceiver);

				}
			} /*else if (currentGun == 2) {
				RaycastHit shot1;
				RaycastHit shot2;
				RaycastHit shot3;
				RaycastHit shot4;
				RaycastHit shot5;
				if (Physics.Raycast (fp.position, fp.forward, out shot, 100.0f)) {
					GameObject clone = Instantiate (impact);
					clone.transform.position = shot.point;

					Destroy (clone, 1f);
					shot.transform.SendMessage ("DeductPoints", damageAmount [currentGun]);

				}


			}*/
		}
		public void FireAutoBullet()
		{
			RaycastHit shot;
			if (Physics.Raycast (fp.position, fp.forward, out shot, 100.0f)) 
			{
				GameObject clone = Instantiate (impact);
				clone.transform.position = shot.point;

				Destroy (clone, 1f);
				shot.transform.SendMessage("DeductPoints", damageAmount[currentGun]);
			}
		}

		public void Update()
		{
			if (autoFire && currentGun == 1) {
				timer += Time.deltaTime;
				if (timer > rifleRate) {
					timer = 0;
					FireAutoBullet ();
				
				}


			}
			if (autoFire && currentGun == 3) {
				timer += Time.deltaTime;
				if (timer > smgRate) {
					timer = 0;
					FireAutoBullet ();
			}

		}
}
}
}
