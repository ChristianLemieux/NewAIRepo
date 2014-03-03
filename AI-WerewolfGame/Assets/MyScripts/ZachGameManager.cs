using UnityEngine;
using System.Collections.Generic;

public class ZachGameManager : MonoBehaviour {

	public int numVillagers;
	public int numWerewolves;
	public GameObject villagerPrefab;
	public GameObject werewolfPrefab;

	private int maxSpawnX, minSpawnX, maxSpawnZ, minSpawnZ;
	private int villagerSpawnPointX, villagerSpawnPointZ, villagerSpawnMod;

	//Text stuffs
	private GUIText dead;
	private GUIText saved;

	//villagers saved/killed
	public int numSaved;
	public int numKilled;

	//Waypoint stuff
	List<WayPoint> allWayPoints;
	int layerMask = 1 << 9; // 9 = "Obstacle layer"


	// Use this for initialization
	void Start () {

		dead = GameObject.FindGameObjectWithTag("Dead").GetComponent<GUIText>();
		saved = GameObject.FindGameObjectWithTag("Saved").GetComponent<GUIText>();

		//Set up spawn boundaries for werewolves.
		maxSpawnX = 1700; minSpawnX = 300;
		maxSpawnZ = 1600; minSpawnZ = 700;

		//Sets up the spawn point for villager, and how far from it they can spawn.
		villagerSpawnPointX = 1450; villagerSpawnPointZ = 460; villagerSpawnMod = 50;

		for (int i = 0; i < numVillagers; i++) 
		{
			//give them a random spawn loc near the spawn point.
			float spawnPosX = villagerSpawnPointX + Random.Range(-villagerSpawnMod,villagerSpawnMod);
			float spawnPosZ = villagerSpawnPointZ + Random.Range(-villagerSpawnMod,villagerSpawnMod);

			//Spawn them off the ground slightly so they dont fall through.
			GameObject.Instantiate(villagerPrefab,new Vector3(spawnPosX,3,spawnPosZ),Quaternion.identity);
		}

		for (int i = 0; i < numWerewolves; i++)
		{
			float spawnPosX = Random.Range(minSpawnX,maxSpawnX);
			float spawnPosZ = Random.Range(minSpawnZ,maxSpawnZ);

			//Spawn them off the ground slightly so they dont fall through.
			GameObject.Instantiate(werewolfPrefab,new Vector3(spawnPosX,5,spawnPosZ),Quaternion.identity);
		}

	}

	//using awake to make sure we dont FUCK IT UP.
	void Awake()
	{
		GameObject[] wps = GameObject.FindGameObjectsWithTag("WayPoint");
		
		for (int i = 0; i < wps.Length-1; i++) 
		{
			for (int j = i+1; j < wps.Length; j++)
			{
				Vector3 direction = (wps[j].transform.position - wps[i].transform.position).normalized;
				float distance = Vector3.Distance(wps[i].transform.position,wps[j].transform.position);
				
				if(!Physics.Raycast(wps[i].transform.position,direction,distance,layerMask))
				{
					//No collision between these two waypoints. OK to form connection.
					WayPoint wp1 = wps[i].GetComponent<WayPoint>();
					WayPoint wp2 = wps[j].GetComponent<WayPoint>();
					
					wp1.addOtherPoint(wps[j]);
					wp2.addOtherPoint(wps[i]);
					Debug.DrawLine (wp1.transform.position,wp2.transform.position,Color.red,30.0f);
				}
			}
		}
	}

	WayPoint getWayPointNear (Vector3 position)
	{
		WayPoint closest = null;
		float closestDist = float.MaxValue;

		foreach (WayPoint wp in allWayPoints) 
		{
			float distance = Vector3.Distance(position,wp.transform.position);

			if(closest == null)
			{
				closest = wp;
				closestDist = distance;
			}
			else 
			{
				if(distance < closestDist)
				{
					closest = wp;
					closestDist = distance;
				}
			}
		}
		return closest;
	}

	// Update is called once per frame
	void Update () {
		dead.text = "Villagers Killed: " + numKilled;
		saved.text = "Villagers Saved: " + numSaved;
	}
}
