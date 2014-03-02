using UnityEngine;
using System.Collections;

public class ZachWerewolf : ZachSteering {

	private GameObject villagerTarget;
	GameObject gameManagerGO;
	ZachGameManager zgm;

	// Use this for initialization
	void Start () {
		gameManagerGO = GameObject.FindGameObjectWithTag ("GameController");
		zgm = gameManagerGO.GetComponent<ZachGameManager>();
		height = 5.0f;
		hasTarget = false;
		velocity = 15;
		rotVelocity = .6f;
	}
	
	// Update is called once per frame
	void Update () {
		if (villagerTarget != null) {
			Seek(villagerTarget);
		}
		else {
			Wander (40);
		}

		base.Steer ();
	}

	void OnTriggerEnter(Collider other)
	{
		//Debug.Log ("a " + other.tag + "has entered the werewolf's trigger!");
		if (other.tag == "Villager") {
			//Debug.Log ("a villager has entered the werewolf's trigger!");
			if (villagerTarget == null) {//if the werewolf didnt already have a target...
					hasTarget = true;
					villagerTarget = other.gameObject;
			} 
		}
		else if(other.tag == "Mayor")
		{
			Debug.Log("Wolf is running from mayor");
			BeginFlee(other.gameObject);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Mayor") 
		{
			Debug.Log("not running from the mayor anymore");
			StopFlee();
		}
	}
}
