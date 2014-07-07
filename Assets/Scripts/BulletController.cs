using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {


	bool facingRight = false;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, 5.0f);

	}
	
	// Update is called once per frame
	void Update () {
		if(rigidbody2D.velocity.x < 0 && facingRight)
			Flip();
		else if (rigidbody2D.velocity.x > 0 && !facingRight)
			Flip();
		//rigidbody2D.velocity = new Vector2(-transform.localScale.x*10, 0);

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Bullet"))
			Destroy (gameObject);
	}
	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
