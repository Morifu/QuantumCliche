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
			Destroy(other.gameObject);
		}

	}

	public void Shoot()
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
