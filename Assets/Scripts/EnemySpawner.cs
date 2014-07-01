using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public InputVCR playerRecording;
	public GameObject enemyPlayer;

	// Use this for initialization
	void Start () {
	
	}

	public void SpawnEnemy(Recording playerRecording)
	{
		GameObject enemy = (GameObject)Instantiate (enemyPlayer);
		InputVCR vcr = enemy.GetComponent<InputVCR> ();
		if (vcr != null)
		{
			vcr.loop = true;
			vcr.Play(new Recording(playerRecording));
		}

	}
}
