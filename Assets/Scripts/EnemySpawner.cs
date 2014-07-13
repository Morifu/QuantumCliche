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
			vcr.loop = true;
			Recording toplay = new Recording(playerRecording);
			vcr.Play(toplay, Random.Range(0,toplay.recordingLength));
		}

	}
}
