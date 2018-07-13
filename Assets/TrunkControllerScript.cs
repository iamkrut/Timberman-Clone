using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkControllerScript : MonoBehaviour {

	private bool flyOut = false;
	private int trunkType; // -1 = normal, 0 = left, 1 = right

	private Vector3 flyDirection;
	private Vector3 rotationDirection;

	public int TrunkType { set { trunkType = value; } get { return trunkType; } }

	void Update () {
		if(flyOut){
			transform.position += flyDirection * 5 * Time.deltaTime;
			transform.Rotate(rotationDirection * 100 * Time.deltaTime);
		}
	}

	public void FlyOut(int dir){ // 0 = left, 1 = right
		gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
		flyOut = true;
		if(dir == 0){
			flyDirection = new Vector3(2, -0.1f, 0);
			rotationDirection = Vector3.back;
		}else{
			flyDirection = new Vector3(-2, -0.1f, 0);
			rotationDirection = Vector3.forward;
		}
	}
}
