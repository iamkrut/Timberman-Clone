using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour {

	public static PlayerControllerScript instance;

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
		GameControllerScript.instance.CutTrunk(0);
	}

	public void TapRight(){
		transform.position = new Vector3(2, transform.position.y, transform.position.z);
		playerSprite.flipX = true;		
		playerAnimator.SetTrigger("Cut");
		GameControllerScript.instance.CutTrunk(1);
	}

	public void Dead(){
		playerSprite.gameObject.SetActive(false);
		deathSpriteGameObject.SetActive(true);
	}
}
