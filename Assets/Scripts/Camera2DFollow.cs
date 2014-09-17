using UnityEngine;
using System.Collections;

public class Camera2DFollow : MonoBehaviour {
	
	public Transform target;
	public float damping = 1;
	public float lookAheadFactor = 3;
	public float lookAheadReturnSpeed = 0.5f;
	public float lookAheadMoveThreshold = 0.1f;
	public SpriteRenderer background;

	Bounds cameraBounds;
	
	float offsetZ;
	Vector3 lastTargetPosition;
	Vector3 currentVelocity;
	Vector3 lookAheadPos;
	
	// Use this for initialization
	void Start () {
		lastTargetPosition = target.position;
		offsetZ = (transform.position - target.position).z;
		transform.parent = null;
		cameraBounds = background.sprite.bounds;
	}
	
	// Update is called once per frame
	void Update () {
		
		// only update lookahead pos if accelerating or changed direction
		float xMoveDelta = (target.position - lastTargetPosition).x;

	    bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

		if (updateLookAheadTarget) {
			lookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
		} else {
			lookAheadPos = Vector3.MoveTowards(lookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);	
		}
		
		Vector3 aheadTargetPos = target.position + lookAheadPos + Vector3.forward * offsetZ;
		Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref currentVelocity, damping);

		float sizeY = Camera.main.orthographicSize; // orthographic size is its height/2 as height is usually smaller, width is defined by aspect ratio
		float sizeX = (2*sizeY) * 16 / 9;
		
		newPos.x = Mathf.Clamp (newPos.x, cameraBounds.min.x + sizeX/2,cameraBounds.max.x - sizeX/2);
		newPos.y = Mathf.Clamp (newPos.y, cameraBounds.min.y + camera.orthographicSize, cameraBounds.max.y - camera.orthographicSize);

		transform.position = newPos;

		
		lastTargetPosition = target.position;	


	}

}
