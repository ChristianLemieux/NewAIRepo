using UnityEngine;
using System.Collections;

public class ZachVillager : ZachSteering {

	GameObject gameManagerGO;
	ZachGameManager zgm;

	// Use this for initialization
	void Start () {
		gameManagerGO = GameObject.FindGameObjectWithTag ("GameController");
		zgm = gameManagerGO.GetComponent<ZachGameManager>();

		velocity = 5;
		rotVelocity = 1;
	}
	
	// Update is called once per frame
	void Update () {
		Wander ();

		base.Steer();

		Debug.DrawRay (transform.position, transform.forward*10);
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Werewolf") {
			GameObject.Destroy (this.gameObject);
			Debug.Log ("wolf caught a poor human");
			zgm.numKilled ++;

		} else if (col.gameObject.tag == "Cart") {
			GameObject.Destroy(this.gameObject);
			Debug.Log ("human escaped!");
			zgm.numSaved ++;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Mayor") {
			Debug.Log ("A villager found the mayor!");
			Follow(other.gameObject);
		} 
		else if (other.tag == "Werewolf") {
			Debug.Log("villager better run!");
			Flee(other.gameObject);
		}
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "Mayor") {
			UnFollow ();
		} else if (other.tag == "Werewolf") {
			StopFlee();
		}
	}
}
