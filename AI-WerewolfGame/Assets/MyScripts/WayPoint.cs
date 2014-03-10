using UnityEngine;
using System.Collections.Generic;

public class WayPoint : MonoBehaviour {
	
	public struct Connection{
		public GameObject otherPoint;
		public float distX;
		public float distZ;
		public float totalDist;
		public WayPoint wp;
	}

	public float distGoal = -1; 
	public float distToReach = float.MaxValue;

	
	public List<Connection> otherWayPoints = new List<Connection> ();
	
	public void addOtherPoint(GameObject otherPt)
	{
		Connection c;
		c.otherPoint = otherPt;
		c.distX = otherPt.transform.position.x - transform.position.x;
		c.distZ = otherPt.transform.position.z - transform.position.z;
		c.totalDist = Vector3.Distance (otherPt.transform.position, transform.position);
		c.wp = otherPt.GetComponent<WayPoint>();
		otherWayPoints.Add (c);
	}
}