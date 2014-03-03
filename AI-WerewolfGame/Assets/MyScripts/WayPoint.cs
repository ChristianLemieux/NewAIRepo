using UnityEngine;
using System.Collections.Generic;

public class WayPoint : MonoBehaviour {

	struct Connection{
		public GameObject otherPoint;
		public float distX;
		public float distZ;
		public float totalDist;
	}

	List<Connection> otherWayPoints = new List<Connection> ();

	public void addOtherPoint(GameObject otherPt)
	{
		Connection c;
		c.otherPoint = otherPt;
		c.distX = otherPt.transform.position.x - transform.position.x;
		c.distZ = otherPt.transform.position.z - transform.position.z;
		c.totalDist = Vector3.Distance (otherPt.transform.position, transform.position);

		otherWayPoints.Add (c);
	}
}
