using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreControllerScript : MonoBehaviour {

	public static ScoreControllerScript instance;

	public TextMeshProUGUI scoreText;

	private int scoreValue;

	void Awake(){
		instance = this;
	}

	void Start(){
		ResetScore();
	}

	private void ResetScore(){
		scoreValue = 0;
	}

	public void AddScore(int value){
		scoreValue++;
		UpdateScoreUI();
	}

	private void UpdateScoreUI(){
		scoreText.text = scoreValue.ToString();
	}
}
