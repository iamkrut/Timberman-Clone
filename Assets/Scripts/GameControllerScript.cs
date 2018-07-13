using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameControllerScript : MonoBehaviour {

	public static GameControllerScript instance;

	public GameObject normalTrunk;
	public GameObject leftBranchTrunk;
	public GameObject rightBranchTrunk;
	public Vector3 firstTrunkPosition;
	public float trunkOffset;

	private GameObject trunk;
	private Vector3 trunkPosition;
	private Vector3 lastTrunkPosition;
	private List<GameObject> tree = new List<GameObject>();

	void Awake(){
		instance = this;
	}

	void Start(){
		GenerateTree();
	}

	public void GenerateTree(){
		trunkPosition = firstTrunkPosition;
		
		trunk =	Instantiate(normalTrunk, trunkPosition, Quaternion.identity) as GameObject;
		tree.Add(trunk);
		lastTrunkPosition = trunkPosition;
		for(int i = 0; i <= 5; i++){
			lastTrunkPosition += new Vector3(0, trunkOffset, 0);
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
			tree.Add(trunk);
		}
	}

	private void CreateTrunk(){
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
		tree.Add(trunk);
	}

	public void CutTrunk(int dir){ // 0 = left, 1 = right

		AudioManagerScript.instance.PlayCutAudio();
		if(tree.ElementAt(1).GetComponent<TrunkControllerScript>().TrunkType == dir){
			GameOver();
		}else{
			ScoreControllerScript.instance.AddScore(1);
			HealthControllerScript.instance.IncreaseHealth();
		}

		GameObject trunkToCut = tree.ElementAt(0);
		trunkToCut.GetComponent<TrunkControllerScript>().FlyOut(dir);
		tree.RemoveAt(0);
		DisplaceTrunks();
		CreateTrunk();
	}

	private void DisplaceTrunks(){
		foreach (GameObject trunk in tree)
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
