using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPlayer1ControllerScript : MonoBehaviour {

	public static MPlayer1ControllerScript instance;

	public SpriteRenderer playerSprite;
	public GameObject deathSpriteGameObject;

	private Animator playerAnimator;

	void Awake(){
		instance = this;
		playerAnimator = gameObject.GetComponent<Animator>();
	}

	public void TapLeft(){
		transform.position = new Vector3(-2, transform.position.y, transform.position.z);
		playerSprite.flipX = false;
		playerAnimator.SetTrigger("Cut");
		MultiplayerGameControllerScript.instance.CutTrunk(1, 0);
	}

	public void TapRight(){
		transform.position = new Vector3(2, transform.position.y, transform.position.z);
		playerSprite.flipX = true;		
		playerAnimator.SetTrigger("Cut");
		MultiplayerGameControllerScript.instance.CutTrunk(1, 1);
	}

	public void Dead(){
		playerSprite.gameObject.SetActive(false);
		deathSpriteGameObject.SetActive(true);
	}
}
