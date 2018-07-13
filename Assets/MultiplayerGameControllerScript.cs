using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class MultiplayerGameControllerScript : MonoBehaviour {

	public static MultiplayerGameControllerScript instance;

	public GameObject normalTrunk;
	public GameObject leftBranchTrunk;
	public GameObject rightBranchTrunk;
	public Vector3 firstTrunkPosition;
	public float trunkOffset;

	private GameObject trunk;
	private Vector3 trunkPosition;
	private Vector3 lastTrunkPosition;
	private List<int> tree = new List<int>();
	private List<GameObject> treePlayer1 = new List<GameObject>();
	private List<GameObject> treePlayer2 = new List<GameObject>();

	private int iteratorPlayer1;
	private int iteratorPlayer2;

	void Awake(){
		instance = this;
	}

	void Start(){
		GenerateTree();
	}

	public void GenerateTree(){
	
		trunkPosition = firstTrunkPosition;
		
		trunk =	Instantiate(normalTrunk, trunkPosition, Quaternion.identity) as GameObject;
		treePlayer1.Add(trunk);
		trunk = Instantiate(trunk, new Vector3(-7.64f, trunkPosition.y, 0), Quaternion.identity) as GameObject;
		treePlayer2.Add(trunk);		
		
		lastTrunkPosition = trunkPosition;
		for(int i = 0; i <= 5; i++){
			lastTrunkPosition = new Vector3(firstTrunkPosition.x, lastTrunkPosition.y + trunkOffset, 0);
			if(Random.Range(0,2) < 1){
				trunk = Instantiate(normalTrunk, lastTrunkPosition, Quaternion.identity) as GameObject;
				trunk.GetComponent<TrunkControllerScript>().TrunkType = -1;
			}else {
				if(Random.Range(0,2) < 1){
					trunk = Instantiate(leftBranchTrunk, lastTrunkPosition, Quaternion.identity) as GameObject;
					trunk.GetComponent<TrunkControllerScript>().TrunkType = 0;
				}else{
					trunk = Instantiate(rightBranchTrunk, lastTrunkPosition, Quaternion.identity) as GameObject;
					trunk.GetComponent<TrunkControllerScript>().TrunkType = 1;
				}
			}
			treePlayer1.Add(trunk);

			trunk = Instantiate(trunk, new Vector3(-7.64f, lastTrunkPosition.y, 0), Quaternion.identity) as GameObject;
			treePlayer2.Add(trunk);		
		}
	}

	private void CreateTrunk(int player){
		int type;
		if(Random.Range(0,2) < 1){
			trunk = Instantiate(normalTrunk, lastTrunkPosition, Quaternion.identity) as GameObject;
			trunk.GetComponent<TrunkControllerScript>().TrunkType = -1;
			type = -1;
		}else {
			if(Random.Range(0,2) < 1){
				trunk = Instantiate(leftBranchTrunk, lastTrunkPosition, Quaternion.identity) as GameObject;
				trunk.GetComponent<TrunkControllerScript>().TrunkType = 0;
				type = 0;
			}else{
				trunk = Instantiate(rightBranchTrunk, lastTrunkPosition, Quaternion.identity) as GameObject;
				trunk.GetComponent<TrunkControllerScript>().TrunkType = 1;
				type = 1;
			}
		}
		tree.Add(type);
		if(player == 1){
			treePlayer1.Add(trunk);
		}else if (player == 2){
			treePlayer2.Add(trunk);
		}
	}

	public void CutTrunk(int player, int dir){ // 0 = left, 1 = right

		AudioManagerScript.instance.PlayCutAudio();
		if(treePlayer1.ElementAt(1).GetComponent<TrunkControllerScript>().TrunkType == dir){
			GameOver();
		}else{
			ScoreControllerScript.instance.AddScore(1);
			HealthControllerScript.instance.IncreaseHealth();
		}

		GameObject trunkToCut = treePlayer1.ElementAt(0);
		trunkToCut.GetComponent<TrunkControllerScript>().FlyOut(dir);
		treePlayer1.RemoveAt(0);
		DisplaceTrunks();
		CreateTrunk(player);
	}

	private void DisplaceTrunks(){
		foreach (GameObject trunk in treePlayer1)
		{
			trunk.transform.position = new Vector3(0, trunk.transform.position.y - trunkOffset, 0);
		}
	}

	public void GameOver(){
		UIManagerScript.instance.OnGameOver();
		PlayerControllerScript.instance.Dead();
		AudioManagerScript.instance.PlayDeathAudio();
		StartCoroutine(WaitBeforeReloading());
	}

	IEnumerator WaitBeforeReloading(){
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene(0);
	}
}
