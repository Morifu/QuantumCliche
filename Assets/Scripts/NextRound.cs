using UnityEngine;
using System.Collections;

public class NextRound : MonoBehaviour {

	public EnemySpawner spawner;

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			other.gameObject.transform.position = Vector3.zero;
			other.gameObject.GetComponent<InputVCR>().Stop();
			spawner.SpawnEnemy(other.gameObject.GetComponent<InputVCR>().GetRecording());
			other.gameObject.GetComponent<InputVCR>().NewRecording();
		}
	}
}
