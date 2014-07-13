using UnityEngine;
using System.Collections;

public class EnemyController : PlatformerCharacter2D {

	[SerializeField]
	public bool deathOnJump = false;
	[SerializeField]
	public bool deathOnShoot = false;
	bool entered = false;
	// Use this for initialization
	void Start () {
	
	}
	
	public void killByJump()
	{
		if(deathOnJump)
			Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(!entered && other.gameObject.CompareTag("Player") )
		{
			Debug.Log("Touched Player");

			entered = true;
		}
		if(other.gameObject.CompareTag("Bullet") && deathOnShoot)
		{
			Destroy(gameObject);
			Destroy(other.gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(entered)
		{
			Debug.Log("Touched Player exit");
			entered = false;

		}
	}

}
