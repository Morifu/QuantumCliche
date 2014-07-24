using UnityEngine;
using System.Collections;

public class PlayerController : PlatformerCharacter2D {

	bool jumpedOnEnemy = false;
	bool bounced = false;
	[SerializeField] LayerMask whoIsEnemy;	
	[SerializeField] GameObject bulletPrefab;
	[SerializeField] float bulletSpeed = 15.0f;

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
			other.gameObject.GetComponent<EnemyController>().killByJump();
		}

	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "Platform")
		{
			platform = other.collider;
		}
	}

//	void OnCollisionStay2D(Collision2D other)
//	{
//		
//		Debug.Log ("Stayed in collision with : " + other.gameObject.tag);
//	}
//
//	void OnCollisionExit2D(Collision2D other)
//	{
//		Debug.Log ("Exited in collision with : " + other.gameObject.tag);
//	}

//	void OnTriggerExit2D(Collider2D other)
//	{
//		Debug.Log ("Exit of :" + other.tag);
//	}

	protected override void CheckCollisions()
	{
		bool shouldignore = crouched || !grounded || (rigidbody2D.velocity.y > 0);
		
		Physics2D.IgnoreLayerCollision( LayerMask.NameToLayer("Ignore"), 
		                               LayerMask.NameToLayer("Ground"), 
		                               shouldignore  
		                               );
		Physics2D.IgnoreCollision (transform.collider2D, platform,crouched&&grounded);
	}

	public override void Shoot()
	{
		GameObject bullet = (GameObject)Instantiate(bulletPrefab,transform.position, new Quaternion(0,0,0,0));
		bullet.rigidbody2D.velocity = new Vector2((facingRight?1:-1) * bulletSpeed, bullet.rigidbody2D.velocity.y);
	}
	
	void Bounce()
	{
		rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, 0);
		rigidbody2D.AddForce(new Vector2(0f, jumpForce*0.5f));
	}
}
