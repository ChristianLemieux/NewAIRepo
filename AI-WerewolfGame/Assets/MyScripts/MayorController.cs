using UnityEngine;
using System.Collections;

public class MayorController : MonoBehaviour {

	private float height; // offset used to keep character above terrain\
	private int layerMask;
	// Use this for initialization
	void Start () {
		height = 3.8f;
		layerMask = 1 << 8; // bit shifting
	}
	
	// Update is called once per frame
	void Update () {

		float rotAmt = Input.GetAxis ("Mouse X") * 3;
		transform.Rotate (0f, rotAmt, 0f);

		if (Input.GetKey("w")) 
		{

			Vector3 forward = transform.forward.normalized;
			if(!Physics.Raycast(transform.position,forward,5,layerMask))
			{			  
				Vector3 pos = transform.position + forward/2;
				transform.position = pos;
			}

		}

		//update height
		Vector3 updatedPos = transform.position;
		updatedPos.y = Terrain.activeTerrain.SampleHeight(transform.position) + height;
		transform.position = updatedPos;
	}

}
