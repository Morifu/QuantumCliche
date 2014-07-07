using UnityEngine;
using System.Collections;

public class EnemyController : PlatformerCharacter2D {

	bool entered = false;
	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if(!entered && other.gameObject.CompareTag("Player") )
		{
			Debug.Log("Touched Player");
			entered = true;
		}
		if(other.gameObject.CompareTag("Bullet"))
		{
			Destroy(gameObject);
			Destroy(other.gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		entered = false;
	}
}
