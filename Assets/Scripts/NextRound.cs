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
			InputVCR vcr = other.gameObject.GetComponent<InputVCR>();
			if(vcr != null)
			{
				other.gameObject.transform.position = Vector3.zero;
				vcr.Stop();
				spawner.SpawnEnemy(vcr.GetRecording());
				vcr.NewRecording();
			}
		
		}
	}
}
