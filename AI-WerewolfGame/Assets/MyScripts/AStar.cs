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
		List<WayPoint> path = new List<WayPoint>();

		WayPoint endGoal = manager.getWayPointNear(goal);
		WayPoint start = manager.getWayPointNear(position);

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

			// find best's best
			foreach(WayPoint.Connection c in best.otherWayPoints){
				if(bb == null){
					bb = c.wp;
				}
				else if(bb.distGoal > wp.distGoal){
					bb = c.wp;
				}
			}
			// check if bb is also connected to current
			bool shared;
			foreach(WayPoint.Connection c in current.otherWayPoints){
				if(c.wp.transform.position.x == bb.transform.position.x && c.wp.transform.position.z == bb.transform.position.z){
					shared = true;
				}
			}
		
			if(shared) current = bb; // if bb is also connected to current, pass it in as the new current instead of best
			else current = best;
			path.Add(current);
		}
		return path;
	}

	private void calcGoalDist(Vector3 goalPos){ 
		foreach(WayPoint wp in manager.allWayPoints){
			wp.distGoal = Vector3.Distance(goalPos, wp.transform.position);
		}
	}	
}