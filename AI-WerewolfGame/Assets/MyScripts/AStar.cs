using UnityEngine;
using System.Collections.Generic;

public class AStar : MonoBehaviour {

	ZachGameManager manager;

	public void Start(){
		manager = GameObject.Find("GameManager").GetComponent<ZachGameManager>();
		if(manager == null){
			Debug.Log ("MANAGER FAIL");
		}
	}

	public WayPoint[] GetPath(Vector3 position,Vector3 goal){
		List<WayPoint> openList = new List<WayPoint>(); // positions that are possible to get to, but not examined yet
		//float totalCost;
		List<WayPoint> closedList = new List<WayPoint>();

		WayPoint endGoal = manager.getWayPointNear(goal);
		WayPoint start = manager.getWayPointNear(position);

		openList.Add(start);

		WayPoint current = start;
		WayPoint best;
		WayPoint bb; // best's best

		while(current != endGoal){

			// find current's best
			foreach(WayPoint.Connection c in current.otherWayPoints){
				
				if(best == null){
					best = c.wp;
				}
				else if(best.distGoal > wp.distGoal){
					best = c.wp;
				}
			}

			/*// find best's best
			foreach(WayPoint.Connection c in best.otherWayPoints){
				if(bb == null){
					bb = c.wp;
				}
				else if(bb.distGoal > wp.distGoal){
					bb = c.wp;
				}
			}
			// check if bb is also connected to current
			if()
		*/

			foreach(WayPoint.Connection c in best.otherWayPoints)
			{
				WayPoint wp = c.wp;
				wp.distToReach = best.distToReach + c.totalDist; // add distance to reach last point to the connection 
																							// dist between the two points.

				if(openList.Exists(wp))
				{

				}
				else if(closedList.Exists(wp)) // the value at this point has been calculated previously
				{
					calcTotalScore(wp, best.distToReach); //TODO
				}
				else//not found in open or closed list -> add to open list
				{
					openList.Add (wp);
				}
			}

			current = best;
			path.Add(current);
		}
		return path;
	}

	private void calcGoalDist(Vector3 goalPos){ 
		foreach(WayPoint wp in manager.allWayPoints){
			wp.distGoal = Vector3.Distance(goalPos, wp.transform.position);
		}
	}	

	private float calcTotalScore(WayPoint wp, prevScore)//TODO
	{
		return null;
	}
}