using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthControllerScript : MonoBehaviour {

	public static HealthControllerScript instance;

	public float decreaseByValue;
	public float increaseByValue;
	public Image healthFillerImage;

	private bool doDecreaseHealth;

	void Awake(){
		instance = this;
	}

	void Start(){
		ResetHealth();
		doDecreaseHealth = true;
	}

	void Update () {
		if(doDecreaseHealth){
			healthFillerImage.fillAmount -= decreaseByValue * Time.deltaTime;

			if(healthFillerImage.fillAmount == 0){
				doDecreaseHealth = false;
				GameControllerScript.instance.GameOver();
			}
		}
	}

	private void ResetHealth(){
		healthFillerImage.fillAmount = 0.5f;
		doDecreaseHealth = false;
	}

	public void IncreaseHealth(){
		healthFillerImage.fillAmount += increaseByValue;
	}
}
