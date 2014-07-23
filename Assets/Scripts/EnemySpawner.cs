using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	
	public GameObject[] enemiesPrefabs;

	// Use this for initialization
	void Start () {
	
	}

	public void SpawnEnemy(Recording playerRecording)
	{
		GameObject enemy = (GameObject)Instantiate (enemiesPrefabs[Random.Range(0,enemiesPrefabs.Length)]);
		InputVCR vcr = enemy.GetComponent<InputVCR> ();
		if (vcr != null)
		{
			Recording toplay = new Recording(playerRecording);
			bool isBackwards = (Random.Range(0,2) == 1)?true:false;
			vcr.Play(toplay, Random.Range(0,toplay.recordingLength), true,isBackwards);
		}

	}
}
