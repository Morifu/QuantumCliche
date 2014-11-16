using UnityEngine;
using System.Collections;

public class PlayerController : PlatformerCharacter2D {

	
	public Transform bodyTransform;
	public Transform gunPoint;

	bool jumpedOnEnemy = false;
	bool bounced = false;
	[SerializeField] LayerMask whoIsEnemy;	
	[SerializeField] GameObject bulletPrefab;
	[SerializeField] float bulletSpeed = 15.0f;


	enum Direction : int 
	{
		Left,
		Up,
		Right,
		Down,
		NE,
		NW,
		SE,
		SW,
		Undefined
	}

	Direction currentDirection = Direction.Undefined;

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
			EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
			if(enemy != null)
				enemy.killByJump();
		}

		if(other.CompareTag("Collectible"))
		{
			GameObject.Destroy(other.gameObject);
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

	protected override void PointAtMouse()
	{
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = -10;
		
		Vector3 objectpos = Camera.main.WorldToScreenPoint(bodyTransform.position);
		mousePos.x = mousePos.x - objectpos.x;
		mousePos.y = mousePos.y - objectpos.y;
		
		float angle = Mathf.Atan2 (mousePos.x, mousePos.y) * Mathf.Rad2Deg;
		
		Debug.Log (angle);
		
		if(angle > -22.5f && angle <= 22.5f)
		{
			anim.SetBool("Up",true);
			anim.SetBool("Left",false);
			anim.SetBool("Right",false);
			anim.SetBool("Down",false);
			anim.SetBool("NE",false);
			anim.SetBool("NW",false);
			anim.SetBool("SW",false);
			anim.SetBool("SE",false);
			currentDirection = Direction.Up;
		}
		else if(angle > 22.5f && angle <= 67.5f)
		{
			anim.SetBool("Up",false);
			anim.SetBool("Left",false);
			anim.SetBool("Right",false);
			anim.SetBool("Down",false);
			anim.SetBool("NE",facingRight);
			anim.SetBool("NW",!facingRight);
			anim.SetBool("SW",false);
			anim.SetBool("SE",false);
			currentDirection = Direction.NE;
		}
		else if(angle > 67.5f && angle <= 112.5f)
		{
			anim.SetBool("Up",false);
			anim.SetBool("Left",!facingRight);
			anim.SetBool("Right",facingRight);
			anim.SetBool("Down",false);
			anim.SetBool("NE",false);
			anim.SetBool("NW",false);
			anim.SetBool("SW",false);
			anim.SetBool("SE",false);
			currentDirection = Direction.Right;
		}
		else if(angle > 112.5f && angle <= 157.5f)
		{
			anim.SetBool("Up",false);
			anim.SetBool("Left",false);
			anim.SetBool("Right",false);
			anim.SetBool("Down",false);
			anim.SetBool("NE",false);
			anim.SetBool("NW",false);
			anim.SetBool("SW",!facingRight);
			anim.SetBool("SE",facingRight);
			currentDirection = Direction.SE;
		}
		else if(angle > 157.5f || angle <= -157.5f)
		{
			anim.SetBool("Up",false);
			anim.SetBool("Left",false);
			anim.SetBool("Right",false);
			anim.SetBool("Down",true);
			anim.SetBool("NE",false);
			anim.SetBool("NW",false);
			anim.SetBool("SW",false);
			anim.SetBool("SE",false);
			currentDirection = Direction.Down;
		}
		else if (angle > -157.5f && angle <= -112.5f)
		{
			anim.SetBool("Up",false);
			anim.SetBool("Left",false);
			anim.SetBool("Right",false);
			anim.SetBool("Down",false);
			anim.SetBool("NE",false);
			anim.SetBool("NW",false);
			anim.SetBool("SW",facingRight);
			anim.SetBool("SE",!facingRight);
			currentDirection = Direction.SW;
		}
		else if (angle > -112.5f && angle <= -67.5f)
		{
			anim.SetBool("Up",false);
			anim.SetBool("Left",facingRight);
			anim.SetBool("Right",!facingRight);
			anim.SetBool("Down",false);
			anim.SetBool("NE",false);
			anim.SetBool("NW",false);
			anim.SetBool("SW",false);
			anim.SetBool("SE",false);
			currentDirection = Direction.Left;
		}
		else if (angle > -67.5f && angle <= -25.5f)
		{
			anim.SetBool("Up",false);
			anim.SetBool("Left",false);
			anim.SetBool("Right",false);
			anim.SetBool("Down",false);
			anim.SetBool("NE",!facingRight);
			anim.SetBool("NW",facingRight);
			anim.SetBool("SW",false);
			anim.SetBool("SE",false);
			currentDirection = Direction.NW;
		}
	}

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
		GameObject bullet = (GameObject)Instantiate(bulletPrefab,gunPoint.position, new Quaternion(0,0,0,0));

		Vector2 direction = Vector2.zero;
		switch(currentDirection)
		{
		case Direction.Down:
			direction = -Vector2.up;
			bullet.transform.Rotate(0f,0f,90.0f);
			break;
		case Direction.Left:
			direction = -Vector2.right;
			break;
		case Direction.Right:
			direction = Vector2.right;
			break;
		case Direction.Up:
			direction = Vector2.up;
			bullet.transform.Rotate(0f,0f,-90.0f);
			break;
		case Direction.NE:
			direction = new Vector2(1f,1f);
			bullet.transform.Rotate(0f,0f,45.0f);
			break;
		case Direction.SE:
			direction = new Vector2(1f,-1f);
			bullet.transform.Rotate(0f,0f,-45.0f);
			break;
		case Direction.SW:
			direction = new Vector2(-1f,-1f);
			bullet.transform.Rotate(0f,0f,45.0f);
			break;
		case Direction.NW:
			direction = new Vector2(-1f,1f);
			bullet.transform.Rotate(0f,0f,-45.0f);
			break;
		default:
			break;
		}
		bullet.rigidbody2D.velocity = direction * bulletSpeed;
		anim.SetTrigger ("Shoot");
	}
	
	void Bounce()
	{
		rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, 0);
		rigidbody2D.AddForce(new Vector2(0f, jumpForce*0.5f));
	}
}
