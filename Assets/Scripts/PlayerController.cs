using UnityEngine;
using System.Collections;

public class PlayerController : PlatformerCharacter2D {

	bool jumpedOnEnemy = false;
	bool bounced = false;
	[SerializeField] LayerMask whoIsEnemy;	

	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		jumpedOnEnemy = Physics2D.OverlapCircle (groundCheck.position, groundedRadius, whoIsEnemy);
		if(grounded)
			bounced = false;
		
		if(jumpedOnEnemy && !grounded && !bounced && transform.rigidbody2D.velocity.y < 0 )
		{
			Debug.Log ("Bounce");
			Bounce();
			bounced = true;
			Destroy(other.gameObject);
		}

	}
	
	void Bounce()
	{
		rigidbody2D.AddForce(new Vector2(0f, jumpForce*1.5f));
	}
}
