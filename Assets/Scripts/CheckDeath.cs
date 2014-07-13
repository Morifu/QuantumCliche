using UnityEngine;
using System.Collections;

public class CheckDeath : MonoBehaviour {

	[SerializeField]
	public bool isPlayer = false;
	[SerializeField]
	public bool deathOnTouch = false;
	[SerializeField]
	public bool deathOnShoot = false;

	GameObject parent;
	// Use this for initialization
	void Start () {
		parent = transform.parent.gameObject;
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if(isPlayer)
		{
			Debug.Log ("Player touched "+other.gameObject.tag);
		}
		else
		{
			if(deathOnShoot && other.gameObject.CompareTag("Bullet"))
				Destroy(parent);
//			else if(deathOnTouch && other.gameObject.CompareTag("Player"))
//				Destroy(parent);

		}
	}
}
