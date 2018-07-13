using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerScript : MonoBehaviour {

	public static UIManagerScript instance;

	public GameObject touchPanel;

	void Awake(){
		instance = this;
	}

	public void OnGameOver(){
		touchPanel.SetActive(false);
	}
}
