using UnityEngine;
using System.Collections;

public class EnemyController : PlatformerCharacter2D {

	bool stayed = false;
	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if(!stayed && other.gameObject.CompareTag("Player") )
		{
			Debug.Log("Touched Player");
			stayed = true;
		}
		
	}

	void OnTriggerExit2D(Collider2D other)
	{
		stayed = false;
	}
}
